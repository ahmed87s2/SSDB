using SSDB.Application.Responses.Identity;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Interfaces.Chat;
using SSDB.Application.Models.Chat;

namespace SSDB.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}