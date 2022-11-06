using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Features.Universities.Commands;

namespace SSDB.Client.Infrastructure.Managers.Catalog.University
{
    public interface IUniversityManager : IManager
    {
        Task<IResult<List<GetAllUniversitiesResponse>>> GetAllAsync();

        Task<IResult<int>> SaveAsync(AddEditUniversityCommand request);

        Task<IResult<int>> DeleteAsync(int id);

        Task<IResult<string>> ExportToExcelAsync(string searchString = "");
    }
}