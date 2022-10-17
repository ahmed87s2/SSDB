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

namespace SSDB.Application.Features.Students.Queries.Export
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
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportStudentsQueryHandler> _localizer;

        public ExportStudentsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
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
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(Students, mappers: new Dictionary<string, Func<Student, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.NameA },
                { _localizer["Collage"], item => item.Batch },
                { _localizer["Description"], item => item.Comments },
                { _localizer["Amount"], item => item.Registration.StudyFees }
            }, sheetName: _localizer["Students"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}