using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Client.Infrastructure.Extensions;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SSDB.Application.Features.Universities.Commands;

namespace SSDB.Client.Infrastructure.Managers.Catalog.University
{
    public class UniversityManager : IUniversityManager
    {
        private readonly HttpClient _httpClient;

        public UniversityManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.UniversitiesEndpoints.Export
                : Routes.UniversitiesEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.UniversitiesEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<List<GetAllUniversitiesResponse>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.UniversitiesEndpoints.GetAll);
            return await response.ToResult<List<GetAllUniversitiesResponse>>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditUniversityCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UniversitiesEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}