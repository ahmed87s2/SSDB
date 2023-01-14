
using SSDB.Application.Features.RegistrationInfo.Queries;
using SSDB.Application.Requests.Catalog;
using SSDB.Application.Requests.Catalog.RegistrationInfo;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Catalog.RegistrationInfo
{
    public interface IRegistrationInfoManager : IManager
    {
        Task<IResult<string>> ExportToExcelAsync(string searchString);
        Task<PaginatedResult<GetAllPagedRegistrationInfoResponse>> GetRegistrationInfosAsync(GetAllPagedRegistrationInfoRequest request);
    }
}