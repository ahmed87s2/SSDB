using SSDB.Application.Models.Chat;
using SSDB.Application.Responses.Identity;
using SSDB.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using SSDB.Application.Interfaces.Chat;

namespace SSDB.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}