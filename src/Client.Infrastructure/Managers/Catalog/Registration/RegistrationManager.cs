using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Application.Requests.Catalog;
using SSDB.Client.Infrastructure.Extensions;
using SSDB.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Registration
{
    public class RegistrationManager : IRegistrationManager
    {
        private readonly HttpClient _httpClient;

        public RegistrationManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.RegistrationsEndpoints.Export
                : Routes.RegistrationsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedRegistrationsResponse>> GetRegistrationsAsync(GetAllPagedRegistrationsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.RegistrationsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedRegistrationsResponse>();
        }

        public async Task<IResult<GetRegistrationByIdResponse>> GetById(int id)
        {
            var response = await _httpClient.GetAsync(Routes.RegistrationsEndpoints.GetById + "/" + id);
            return await response.ToResult<GetRegistrationByIdResponse>();
        }

        public async Task<IResult<AddRegistrationCommand>> GetForAddEdit(int id)
        {
            var response = await _httpClient.GetAsync(Routes.RegistrationsEndpoints.GetForAddEdit + id);
            return await response.ToResult<AddRegistrationCommand>();
        }

        public async Task<IResult<int>> SaveAsync(AddRegistrationCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.RegistrationsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}