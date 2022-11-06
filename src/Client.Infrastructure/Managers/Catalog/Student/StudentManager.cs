using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Application.Features.Students.Commands;
using SSDB.Application.Features.Students.Queries;
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

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.StudentsEndpoints.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.StudentsEndpoints.Export
                : Routes.StudentsEndpoints.ExportFiltered(searchString));
            return await response.ToResult<string>();
        }

        public async Task<IResult<string>> GetStudentImageAsync(string id)
        {
            var response = await _httpClient.GetAsync(Routes.StudentsEndpoints.GetStudentImage(id));
            return await response.ToResult<string>();
        }

        public async Task<IResult<GetStudentByIdResponse>> GetById(GetStudentByIdQuery request)
        {
            var response = await _httpClient.GetAsync(Routes.StudentsEndpoints.GetById);
            return await response.ToResult<GetStudentByIdResponse>();
        }

        public async Task<IResult<AddEditStudentCommand>> GetForAddEdit(string studentNumber)
        {
            var response = await _httpClient.GetAsync(Routes.StudentsEndpoints.GetForAddEdit+ studentNumber);
            return await response.ToResult<AddEditStudentCommand>();
        }

        public async Task<PaginatedResult<GetAllPagedStudentsResponse>> GetStudentsAsync(GetAllPagedStudentsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.StudentsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedStudentsResponse>();
        }

        public async Task<IResult<string>> SaveAsync(AddEditStudentCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.StudentsEndpoints.Save, request);
            return await response.ToResult<string>();
        }
    }
}