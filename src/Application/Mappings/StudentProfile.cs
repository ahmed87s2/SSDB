using AutoMapper;
using SSDB.Application.Features.Students.Commands.AddEdit;
using SSDB.Application.Models;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<AddEditStudentCommand, Student>().ReverseMap();
            CreateMap<StdForReg, Student>().ReverseMap()
                .ForMember(d => d.CurrencyNo, opt => opt.MapFrom(x => x.Currency.Name))
                .ForMember(d => d.Semester, opt => opt.MapFrom(x => x.Semester.Name))
                .ForMember(d => d.StudentNumber, opt => opt.MapFrom(x => x.Number))
                .ForMember(d => d.StudentNameA, opt => opt.MapFrom(x => x.NameA))
                .ForMember(d => d.RegistrationFees, opt => opt.MapFrom(x => x.Registration.Fees))
                .ForMember(d => d.Facultynumber, opt => opt.MapFrom(x => x.Fuculty.NameA));
        }
    }
}