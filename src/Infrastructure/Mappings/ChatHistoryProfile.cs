using AutoMapper;
using SSDB.Application.Interfaces.Chat;
using SSDB.Application.Models.Chat;
using SSDB.Infrastructure.Models.Identity;

namespace SSDB.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}