using AutoMapper;
using SSDB.Application.Features.Registrations.Commands;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Application.Features.Students.Queries;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;

namespace SSDB.Application.Mappings
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<AddEditRegistrationCommand, Registration>().ReverseMap();
            CreateMap<GetRegistrationByIdResponse, Registration>().ReverseMap();
            CreateMap<GetRegistrationByIdResponse, Registration>().ReverseMap();
            CreateMap<GetAllPagedRegistrationsResponse, Registration>().ReverseMap();
            CreateMap<GetRegistrationByIdResponse, AddEditRegistrationCommand>().ReverseMap();
            CreateMap<PaginatedResult<GetAllPagedRegistrationsResponse>, PaginatedResult<Registration>>().ReverseMap();

        }
    }
}