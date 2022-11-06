using AutoMapper;
using SSDB.Application.Features.Universities.Commands;
using SSDB.Application.Features.Universities.Queries.GetAll;
using SSDB.Application.Features.Universities.Queries.GetById;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Mappings
{
    public class UniversityProfile : Profile
    {
        public UniversityProfile()
        {
            CreateMap<AddEditUniversityCommand, University>().ReverseMap();
            CreateMap<GetUniversityByIdResponse, University>().ReverseMap();
            CreateMap<GetAllUniversitiesResponse, University>().ReverseMap();
        }
    }
}