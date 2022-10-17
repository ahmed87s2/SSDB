using SSDB.Application.Features.Students.Commands.AddEdit;
using SSDB.Application.Features.Students.Queries.GetAllPaged;
using SSDB.Application.Requests.Catalog;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Student
{
    public interface IStudentManager : IManager
    {
        Task<PaginatedResult<GetAllPagedStudentsResponse>> GetStudentsAsync(GetAllPagedStudentsRequest request);

        Task<IResult<string>> GetStudentImageAsync(int id);

        Task<IResult<int>> SaveAsync(AddEditStudentCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}