using AutoMapper;
using SSDB.Infrastructure.Models.Identity;
using SSDB.Application.Responses.Identity;

namespace SSDB.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, BlazorHeroRole>().ReverseMap();
        }
    }
}