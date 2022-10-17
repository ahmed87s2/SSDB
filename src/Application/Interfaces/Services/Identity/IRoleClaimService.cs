using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Interfaces.Common;
using SSDB.Application.Requests.Identity;
using SSDB.Application.Responses.Identity;
using SSDB.Shared.Wrapper;

namespace SSDB.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}