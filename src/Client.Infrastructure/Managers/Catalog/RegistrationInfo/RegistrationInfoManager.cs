using SSDB.Application.Features.RegistrationInfo.Queries;
using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Requests.Catalog;
using SSDB.Application.Requests.Catalog.RegistrationInfo;
using SSDB.Client.Infrastructure.Extensions;
using SSDB.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.RegistrationInfo
{
    public class RegistrationInfoManager : IRegistrationInfoManager
    {
        private readonly HttpClient _httpClient;

        public RegistrationInfoManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.RegistrationInfosEndpoints.Export
                : Routes.RegistrationInfosEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedRegistrationInfoResponse>> GetRegistrationInfosAsync(GetAllPagedRegistrationInfoRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.RegistrationInfosEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedRegistrationInfoResponse>();
        }

        public async Task<IResult<string>> UpdateRegistrationAsync(UpdateRegistrationInfoCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.RegistrationInfosEndpoints.UpdateRegistrationInfo, request);
            return await response.ToResult<string>();
        }

    }
}