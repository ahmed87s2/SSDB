using SSDB.Application.Interfaces.Common;
using SSDB.Application.Requests.Identity;
using SSDB.Application.Responses.Identity;
using SSDB.Shared.Wrapper;
using System.Threading.Tasks;

namespace SSDB.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}