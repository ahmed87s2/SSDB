using SSDB.Application.Requests.Identity;
using SSDB.Application.Responses.Identity;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSDB.Client.Infrastructure.Managers.Identity.Roles
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<RoleResponse>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(RoleRequest role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionResponse>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}