using SSDB.Shared.Wrapper;
using System.Threading.Tasks;
using SSDB.Application.Features.Dashboards.Queries.GetData;

namespace SSDB.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}