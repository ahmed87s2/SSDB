using SSDB.Application.Features.Payments.Commands;
using SSDB.Application.Features.Payments.Queries;
using SSDB.Application.Requests.Catalog;
using SSDB.Client.Infrastructure.Extensions;
using SSDB.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Payment
{
    public class PaymentManager : IPaymentManager
    {
        private readonly HttpClient _httpClient;

        public PaymentManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.PaymentsEndpoints.Export
                : Routes.PaymentsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedPaymentsResponse>> GetPaymentsAsync(GetAllPagedPaymentsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.PaymentsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedPaymentsResponse>();
        }

        public async Task<IResult<GetPaymentByIdResponse>> GetById(int id)
        {
            var response = await _httpClient.GetAsync(Routes.PaymentsEndpoints.GetById + "/" + id);
            return await response.ToResult<GetPaymentByIdResponse>();
        }

        public async Task<IResult<AddPaymentCommand>> GetForAddEdit(int id)
        {
            var response = await _httpClient.GetAsync(Routes.PaymentsEndpoints.GetForAddEdit + id);
            return await response.ToResult<AddPaymentCommand>();
        }

        public async Task<IResult<int>> SaveAsync(AddPaymentCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.PaymentsEndpoints.Save, request);
            return await response.ToResult<int>();
        }

    }
}