using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SSDB.Application.Enums;
using SSDB.Application.Features.Utilities.GetDropDownListInfo;
using SSDB.Application.Interfaces.Repositories;
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

        public GetDropDownListInfoQueryHandler(IUnitOfWork<int> unitOfWork, IUnitOfWork<string> studentUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _studentUnitOfWork = studentUnitOfWork;
        }

        public async Task<Result<List<DropDownListItemResponse>>> Handle(GetDropdownListDataQuery request, CancellationToken cancellationToken)
        {
            switch (request.Type)
            {
                case ListType.Addmission:
                    return Result<List<DropDownListItemResponse>>.Success(await getAddmissionsInfo());
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
                case ListType.Specializations:
                    return Result<List<DropDownListItemResponse>>.Success(await getSpecializationsInfo());
                case ListType.Students:
                    return Result<List<DropDownListItemResponse>>.Success(await getStudentsInfo());
                default:
                    return Result<List<DropDownListItemResponse>>.Fail("Invalid Request");
            }
        }
        
        private async Task<List<DropDownListItemResponse>> getAddmissionsInfo()
        {
            return await _unitOfWork.Repository<Addmission>().Entities
                .Select(x => new DropDownListItemResponse{Key = x.Name, Value = x.Id.ToString()}).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getBatchesInfo()
        {
            return await _unitOfWork.Repository<Batch>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.Name, Value = x.Id.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getCurrenciesInfo()
        {
            return await _unitOfWork.Repository<Currency>().Entities
                .Select(x => new DropDownListItemResponse { Key = $"{x.Name} | {x.Adjust}" , Value = x.Id.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getDepartmentsInfo()
        {
            return await _unitOfWork.Repository<Department>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getFucultiesInfo()
        {
            return await _unitOfWork.Repository<Fuculty>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getNationalitiesInfo()
        {
            return await _unitOfWork.Repository<Nationality>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getProgramsInfo()
        {
            return await _unitOfWork.Repository<Program>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getSemestersInfo()
        {
            return await _unitOfWork.Repository<Semester>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.Name, Value = x.Id.ToString() }).ToListAsync();
        }
        private async Task<List<DropDownListItemResponse>> getSpecializationsInfo()
        {
            return await _unitOfWork.Repository<Specialization>().Entities
                .Select(x => new DropDownListItemResponse { Key = x.NameA, Value = x.Id.ToString() }).ToListAsync();
        }

        private async Task<List<DropDownListItemResponse>> getStudentsInfo()
        {
            var studetns = await _studentUnitOfWork.Repository<Student>().Entities
                .Select(x => new DropDownListItemResponse
                {
                    Key = x.NameA,
                    Value = x.Id
                }).ToListAsync();
            return studetns;
        }
    }
}
