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

namespace SSDB.Application.Features.Students.Queries
{
    public class GetAllStudentsQuery : IRequest<PaginatedResult<GetAllPagedStudentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public string[] OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllStudentsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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

    internal class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, PaginatedResult<GetAllPagedStudentsResponse>>
    {
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllStudentsQueryHandler(IUnitOfWork<string> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllPagedStudentsResponse>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var studentFilterSpec = new StudentFilterSpecification(request.SearchString);
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Student>().Entities
                    .Include(x => x.Addmission)
                    .Include(x => x.Department)
                    .Include(x => x.Batch)
                    .Include(x => x.Currency)
                    .Include(x => x.Fuculty)
                    .Include(x => x.Program)
                    .Include(x => x.Semester)
                    .Include(x => x.Specialization)
                   .Specify(studentFilterSpec)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                var mapped = _mapper.Map<PaginatedResult<GetAllPagedStudentsResponse>>(data);
                return mapped;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Student>().Entities
                    .Include(x => x.Addmission)
                    .Include(x => x.Department)
                    .Include(x => x.Batch)
                    .Include(x => x.Currency)
                    .Include(x => x.Fuculty)
                    .Include(x => x.Program)
                    .Include(x => x.Semester)
                    .Include(x => x.Specialization)
                   .Specify(studentFilterSpec)
                   .OrderBy(ordering)
                   .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                var mapped = _mapper.Map<PaginatedResult<GetAllPagedStudentsResponse>>(data);
                return mapped;

            }
        }
    }
}