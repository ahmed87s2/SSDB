using SSDB.Application.Interfaces.Common;

namespace SSDB.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}