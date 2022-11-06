using SSDB.Application.Features.Students.Commands;
using SSDB.Application.Features.Students.Queries;
using SSDB.Application.Requests.Catalog;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.Student
{
    public interface IStudentManager : IManager
    {
        Task<PaginatedResult<GetAllPagedStudentsResponse>> GetStudentsAsync(GetAllPagedStudentsRequest request);

        Task<IResult<string>> GetStudentImageAsync(string id);

        Task<IResult<string>> SaveAsync(AddEditStudentCommand request);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
        Task<IResult<AddEditStudentCommand>> GetForAddEdit(string studentNumber);
        Task<IResult<GetStudentByIdResponse>> GetById(GetStudentByIdQuery request);
    }
}