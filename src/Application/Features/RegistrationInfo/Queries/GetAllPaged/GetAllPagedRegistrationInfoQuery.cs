using SSDB.Application.Extensions;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Specifications.Catalog;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SSDB.Application.Interfaces.Services.Identity;
using SSDB.Application.Interfaces.Services;
using SSDB.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Newtonsoft.Json;
using SSDB.Application.Features.Registrations.Commands;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace SSDB.Application.Features.RegistrationInfo.Queries
{
    public class GetAllPagedRegistrationInfoQuery : IRequest<PaginatedResult<GetAllPagedRegistrationInfoResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllPagedRegistrationInfoQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class GetAllRegistrationInfoQueryHandler : IRequestHandler<GetAllPagedRegistrationInfoQuery, PaginatedResult<GetAllPagedRegistrationInfoResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUnitOfWork<string> _studentUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;
        public GetAllRegistrationInfoQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, ICurrentUserService currentUser, IUserService userService, IUnitOfWork<string> studentUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
            _userService = userService;
            _studentUnitOfWork = studentUnitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedRegistrationInfoResponse>> Handle(GetAllPagedRegistrationInfoQuery request, CancellationToken cancellationToken)
        {
            var isAdmin = await _userService.IsAdminAsync(_currentUser.UserId);
            var expression = getUniversityExpression(isAdmin);
            var universities = await _unitOfWork.Repository<University>().Entities.Where(expression).ToListAsync();
            List<StudentsRegistrationInfo> finalResult = new();
            foreach (var university in universities)
            {
                var result = (university.Type.ToLower() == UniversityType.Inhouse.ToString().ToLower()) ?
                    GetInhouseReegistrationInfo(isAdmin, request.SearchString) :
                    GetOutSourseRegistrationInfo(isAdmin, request.SearchString);
                finalResult.AddRange(await result.ToListAsync());
            }

            if (request.OrderBy?.Any() != true)
            {
                var data = finalResult
                   .ToPaginatedList(request.PageNumber, request.PageSize);

                var mappedData = _mapper.Map<PaginatedResult<GetAllPagedRegistrationInfoResponse>>(data);
                return mappedData;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await finalResult.AsQueryable()
                   .OrderBy(ordering)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);

                var mappedData = _mapper.Map<PaginatedResult<GetAllPagedRegistrationInfoResponse>>(data);
                return mappedData;
            }
        }

        private Expression<Func<Student, bool>> getExpression(bool isAdmin)
        {
            if (!isAdmin)
            {
                return (x => x.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return (x => x.Id != null);
        }

        private Expression<Func<University, bool>> getUniversityExpression(bool isAdmin)
        {
            if (!isAdmin)
            {
                return (x => x.Id == int.Parse(_currentUser.UniversityId));
            }
            return (x => x.Id != 0);
        }
        private Expression<Func<StudentsRegistrationInfo, bool>> getStudentsRegistrationInfoExpression(bool isAdmin)
        {
            if (!isAdmin)
            {
                return (x => x.Id == int.Parse(_currentUser.UniversityId));
            }
            return (x => x.Id != 0);
        }

        private IQueryable<StudentsRegistrationInfo> GetInhouseReegistrationInfo(bool isAdmin, string searchString)
        {
            var expression = getExpression(isAdmin);
            var studentFilterSpec = new StudentFilterSpecification(searchString);
            IQueryable<StudentsRegistrationInfo> registrationInfo = _studentUnitOfWork.Repository<Student>().Entities
                                .Specify(studentFilterSpec)
                                .Where(expression)
                                .Where(x => x.University.Type.ToLower() == UniversityType.Inhouse.ToString().ToLower())
                                .Include(x => x.Semester)
                                .Include(x => x.Currency)
                                .Include(x => x.Fuculty)
                                .Include(x => x.University)
                                .Select(x => new StudentsRegistrationInfo
                                {
                                    UniversityId = x.UniversityId,
                                    StudentNumber = x.Id,
                                    Name = x.NameA,
                                    RegistrationFees = double.Parse(x.RegistrationFees.ToString()),
                                    CurrencyName = x.Currency.Name,
                                    NoStudyFees = x.NoStudyFees,
                                    Note = x.Comments,
                                    FucultyName = x.Fuculty.NameA,
                                    Semester = x.Semester.Name,
                                    Status = x.Status,
                                });

            return registrationInfo;
        }

        private IQueryable<StudentsRegistrationInfo> GetOutSourseRegistrationInfo(bool isAdmin, string searchString)
        {
            var expression = getStudentsRegistrationInfoExpression(isAdmin);
            var RegistrationFilterSpec = new RegistrationInfoFilterSpecification(searchString);
            var registrationInfo = _unitOfWork.Repository<StudentsRegistrationInfo>().Entities
                .Specify(RegistrationFilterSpec)
                .Where(expression);

            return registrationInfo;
        }
    }
}