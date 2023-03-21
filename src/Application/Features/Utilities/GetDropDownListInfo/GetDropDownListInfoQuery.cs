using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSDB.Application.Enums;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Interfaces.Services;
using SSDB.Application.Interfaces.Services.Identity;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSDB.Application.Features.Utilities.Queries
{
    public class GetDropdownListDataQuery : IRequest<Result<List<DropDownListItemResponse>>>
    {
        public ListType Type { get; set; }
    }
    internal class GetDropDownListInfoQueryHandler : IRequestHandler<GetDropdownListDataQuery, Result<List<DropDownListItemResponse>>>
    {
        private readonly IUnitOfWork<string> _studentUnitOfWork;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IUserService _userService;

        public GetDropDownListInfoQueryHandler(IUnitOfWork<int> unitOfWork, IUnitOfWork<string> studentUnitOfWork, ICurrentUserService currentUser, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _studentUnitOfWork = studentUnitOfWork;
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<Result<List<DropDownListItemResponse>>> Handle(GetDropdownListDataQuery request, CancellationToken cancellationToken)
        {
            switch (request.Type)
            {                
                case ListType.Batches:
                    return Result<List<DropDownListItemResponse>>.Success(await getBatchesInfo());
                case ListType.Currency:
                    return Result<List<DropDownListItemResponse>>.Success(await getCurrenciesInfo());                
                case ListType.Departments:
                    return Result<List<DropDownListItemResponse>>.Success(await getDepartmentsInfo());
                case ListType.Fuculties:
                    return Result<List<DropDownListItemResponse>>.Success(await getFucultiesInfo());
                case ListType.Nationalities:
                    return Result<List<DropDownListItemResponse>>.Success(await getNationalitiesInfo());
                case ListType.Programs:
                    return Result<List<DropDownListItemResponse>>.Success(await getProgramsInfo());
                case ListType.Semesters:
                    return Result<List<DropDownListItemResponse>>.Success(await getSemestersInfo());
                case ListType.Students:
                    return Result<List<DropDownListItemResponse>>.Success(await getStudentsInfo());
                case ListType.Universities:
                    return Result<List<DropDownListItemResponse>>.Success(await getUniversitiesInfo());                
                default:
                    return Result<List<DropDownListItemResponse>>.Fail("Invalid Request");
            }
        }
        

        private async Task<List<DropDownListItemResponse>> getBatchesInfo()
        {
            var batches = _unitOfWork.Repository<Batch>().Entities;
                
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                batches = batches.Include(x => x.Program)
                .ThenInclude(x => x.Department)
                .ThenInclude(x => x.Fuculty)
                .Where(x => x.Program.Department.Fuculty.UniversityId == int.Parse(_currentUser.UniversityId));
            }

            return await batches
                .Select(x => new DropDownListItemResponse { Key = $"{x.Name}|{x.RegistrationFees}|{x.StudyFees}", Value = x.Id.ToString(), Reference= x.ProgramId.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getCurrenciesInfo()
        {
            var currencies = _unitOfWork.Repository<Currency>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                currencies = currencies.Where(x => x.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return await currencies.Select(x => new DropDownListItemResponse { Key = $"{x.Name}" , Value = x.Id.ToString(), Reference = x.UniversityId.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getDepartmentsInfo()
        {
            var departments = _unitOfWork.Repository<Department>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                departments = departments
                .Include(x => x.Fuculty)
                .ThenInclude(x => x.University).Where(x => x.Fuculty.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return await departments.Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString(), Reference = x.FucultyId.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getFucultiesInfo()
        {
            var fuculties = _unitOfWork.Repository<Fuculty>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                fuculties = fuculties.Where(x => x.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return await fuculties.Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString(), Reference=x.UniversityId.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getNationalitiesInfo()
        {
            return await _unitOfWork.Repository<Nationality>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getProgramsInfo()
        {
            var programs = _unitOfWork.Repository<Program>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                programs = programs
                    .Include(x=>x.Department)
                    .ThenInclude(x=>x.Fuculty)
                    .ThenInclude(x=>x.University)                    
                    .Where(x => x.Department.Fuculty.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return await programs.Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString(), Reference = x.DepartmentId.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getSemestersInfo()
        {
            var semesters = _unitOfWork.Repository<Semester>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                semesters = semesters
                    .Include(x => x.Batch)
                    .ThenInclude(x => x.Program)
                    .ThenInclude(x => x.Department)
                    .ThenInclude(x => x.Fuculty).Where(x => x.Batch.Program.Department.Fuculty.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            return await semesters.Select(x => new DropDownListItemResponse { Key = x.Name, Value = x.Id.ToString(), Reference = x.BatchId.ToString() }).ToListAsync();
        }
        
        private async Task<List<DropDownListItemResponse>> getUniversitiesInfo()
        {
            var universities = _unitOfWork.Repository<University>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                universities = universities.Where(x => x.Id == int.Parse(_currentUser.UniversityId));
            }
            return await universities.Select(x => new DropDownListItemResponse { Key = x.Name, Value = x.Id.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getStudentsInfo()
        {
            var students = _studentUnitOfWork.Repository<Student>().Entities;
            if (!(await _userService.IsAdminAsync(_currentUser.UserId)))
            {
                students = students.Where(x => x.UniversityId == int.Parse(_currentUser.UniversityId));
            }
            var studetns = await students.Select(x => new DropDownListItemResponse
                {
                    Key = x.FirstNameA,
                    Value = x.Id
                }).ToListAsync();
            return studetns;
        }
    }
}
