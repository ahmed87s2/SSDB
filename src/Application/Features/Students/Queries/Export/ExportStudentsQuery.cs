using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Interfaces.Services;
using SSDB.Domain.Entities.Catalog;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SSDB.Application.Extensions;
using SSDB.Application.Specifications.Catalog;
using SSDB.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Features.Students.Queries
{
    public class ExportStudentsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportStudentsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportStudentsQueryHandler : IRequestHandler<ExportStudentsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<string> _unitOfWork;
        private readonly IStringLocalizer<ExportStudentsQueryHandler> _localizer;

        public ExportStudentsQueryHandler(IExcelService excelService
            , IUnitOfWork<string> unitOfWork
            , IStringLocalizer<ExportStudentsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportStudentsQuery request, CancellationToken cancellationToken)
        {
            var StudentFilterSpec = new StudentFilterSpecification(request.SearchString);
            var Students = await _unitOfWork.Repository<Student>().Entities
                .Specify(StudentFilterSpec)
                .Include(x=>x.Program)
                .Include(x=>x.Department)
                .Include(x=>x.Specialization)
                .Include(x=>x.Semester)
                .Include(x=>x.Batch)
                .Include(x=>x.Fuculty)
                .Include(x=>x.Addmission)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(Students, mappers: new Dictionary<string, Func<Student, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Number"], item => item.Id },
                { _localizer["Name"], item => item.FirstNameA },
                { _localizer["Program"], item => item.Program.NameA },
                { _localizer["Department"], item => item.Department.NameA },
                { _localizer["Specialization"], item => item.Specialization.NameA },
                { _localizer["Semester"], item => item.Semester.Name },
                { _localizer["Batch"], item => item.Batch.Name },
                { _localizer["Fuculty"], item => item.Fuculty.NameA },
                { _localizer["Addmission"], item => item.Addmission.Name },
                { _localizer["Status"], item => item.Status }
            }, sheetName: _localizer["Students"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}