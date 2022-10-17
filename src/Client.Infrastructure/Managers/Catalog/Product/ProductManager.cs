using SSDB.Application.Features.Students.Commands.AddEdit;
using SSDB.Application.Features.Students.Queries.GetAllPaged;
using SSDB.Application.Requests.Catalog;
using SSDB.Client.Infrastructure.Extensions;
using SSDB.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Student
{
    public class StudentManager : IStudentManager
    {
        private readonly HttpClient _httpClient;

        public StudentManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.StudentsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.StudentsEndpoints.Export
                : Routes.StudentsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> GetStudentImageAsync(int id)
        {
            var response = await _httpClient.GetAsync(Routes.StudentsEndpoints.GetStudentImage(id));
            return await response.ToResult<string>();
        }

        public async Task<PaginatedResult<GetAllPagedStudentsResponse>> GetStudentsAsync(GetAllPagedStudentsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.StudentsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedStudentsResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditStudentCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.StudentsEndpoints.Save, request);
            return await response.ToResult<int>();
        }
    }
}