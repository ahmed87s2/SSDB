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

namespace SSDB.Application.Features.Registrations.Queries
{
    public class ExportRegistrationsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportRegistrationsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportRegistrationsQueryHandler : IRequestHandler<ExportRegistrationsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportRegistrationsQueryHandler> _localizer;

        public ExportRegistrationsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportRegistrationsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportRegistrationsQuery request, CancellationToken cancellationToken)
        {
            var RegistrationFilterSpec = new RegistrationFilterSpecification(request.SearchString);
            var Registrations = await _unitOfWork.Repository<Registration>().Entities
                .Specify(RegistrationFilterSpec)
                .Include(x=>x.Student)
                .Include(x=>x.Semester)
                .Include(x=>x.Currency)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(Registrations, mappers: new Dictionary<string, Func<Registration, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Date"], item => item.CreatedOn },
                { _localizer["Student Number"], item => item.Student.Id },
                { _localizer["Name"], item => item.Student.FirstNameA },
                { _localizer["Status"], item => item.Status },
                { _localizer["Fees"], item => item.Fees },
                { _localizer["Study Fees"], item => item.StudyFees },
                { _localizer["Currency"], item => item.Currency.Name},
                { _localizer["Payment No"], item => item.PaymentNo },
                { _localizer["BranchId"], item => item.BranchId },
                { _localizer["linkNo"], item => item.linkNo }
            }, sheetName: _localizer["Registrations"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}