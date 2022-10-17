using AutoMapper;
using SSDB.Infrastructure.Models.Audit;
using SSDB.Application.Responses.Audit;

namespace SSDB.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}