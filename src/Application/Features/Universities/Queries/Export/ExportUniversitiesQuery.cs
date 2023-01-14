using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SSDB.Application.Extensions;
using SSDB.Application.Interfaces.Repositories;
using SSDB.Application.Interfaces.Services;
using SSDB.Application.Specifications.Catalog;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SSDB.Application.Features.Universities.Queries.Export
{
    public class ExportUniversitiesQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportUniversitiesQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportUniversitiesQueryHandler : IRequestHandler<ExportUniversitiesQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportUniversitiesQueryHandler> _localizer;

        public ExportUniversitiesQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportUniversitiesQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportUniversitiesQuery request, CancellationToken cancellationToken)
        {
            var UniversityFilterSpec = new UniversityFilterSpecification(request.SearchString);
            var Universities = await _unitOfWork.Repository<University>().Entities
                .Specify(UniversityFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(Universities, mappers: new Dictionary<string, Func<University, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Type"], item => item.Type },
                { _localizer["IsActive"], item => item.IsActive }
            }, sheetName: _localizer["Universities"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
