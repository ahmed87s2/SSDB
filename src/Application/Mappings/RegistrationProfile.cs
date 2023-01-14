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
            CreateMap<AddRegistrationCommand, Registration>().ReverseMap();
            CreateMap<GetRegistrationByIdResponse, Registration>().ReverseMap();
            CreateMap<GetRegistrationByIdResponse, Registration>().ReverseMap();
            CreateMap<GetAllPagedRegistrationsResponse, Registration>().ReverseMap();
            CreateMap<GetRegistrationByIdResponse, AddRegistrationCommand>().ReverseMap();
            CreateMap<PaginatedResult<GetAllPagedRegistrationsResponse>, PaginatedResult<Registration>>().ReverseMap();
            CreateMap<StudentsRegistrationInfo, Registration>().ReverseMap()
                 .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Student.NameA))
                 .ForMember(d => d.RegistrationFees, opt => opt.MapFrom(s => s.Fees))
                 .ForMember(d => d.CurrencyName, opt => opt.MapFrom(s => s.Currency.Name))
                 .ForMember(d => d.Note, opt => opt.MapFrom(s => s.Comments))
                 .ForMember(d => d.FucultyName, opt => opt.MapFrom(s => s.Student.Fuculty.NameA));

        }
    }
}