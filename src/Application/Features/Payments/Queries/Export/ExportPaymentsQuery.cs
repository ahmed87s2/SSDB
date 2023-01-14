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

namespace SSDB.Application.Features.Payments.Queries
{
    public class ExportPaymentsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportPaymentsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportPaymentsQueryHandler : IRequestHandler<ExportPaymentsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportPaymentsQueryHandler> _localizer;

        public ExportPaymentsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportPaymentsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportPaymentsQuery request, CancellationToken cancellationToken)
        {
            var PaymentFilterSpec = new PaymentFilterSpecification(request.SearchString);
            var Payments = await _unitOfWork.Repository<Payment>().Entities
                .Specify(PaymentFilterSpec)
                .ToListAsync( cancellationToken);
            var data = await _excelService.ExportAsync(Payments, mappers: new Dictionary<string, Func<Payment, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Date"], item => item.CreatedOn },
                { _localizer["Student Number"], item => item.StudentNumber },
                { _localizer["Name"], item => item.Name },
                { _localizer["Status"], item => item.Status },
                { _localizer["Fees"], item => item.RegistrationFees },
                { _localizer["No Study Fees"], item => item.NoStudyFees },
                { _localizer["Currency"], item => item.CurrencyName},
                { _localizer["Payment No"], item => item.PaymentNo },
                { _localizer["BranchId"], item => item.BranchId }
            }, sheetName: _localizer["Payments"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}