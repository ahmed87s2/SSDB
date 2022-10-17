using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Requests.Identity;
using SSDB.Application.Responses.Identity;
using SSDB.Shared.Wrapper;

namespace SSDB.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}