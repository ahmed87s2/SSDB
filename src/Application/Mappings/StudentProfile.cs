using AutoMapper;
using SSDB.Application.Features.Registrations.Queries;
using SSDB.Application.Features.Students.Commands;
using SSDB.Application.Features.Students.Queries;
using SSDB.Application.Models;
using SSDB.Domain.Entities.Catalog;
using SSDB.Shared.Wrapper;

namespace SSDB.Application.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<AddEditStudentCommand, Student>().ReverseMap();
            CreateMap<GetAllPagedStudentsResponse, Student>().ReverseMap();
            CreateMap<GetStudentByIdResponse, Student>().ReverseMap();
            CreateMap<GetRegistrationById_StudentResponse, Student>().ReverseMap();
            CreateMap<PaginatedResult<GetAllPagedStudentsResponse>, PaginatedResult<Student>>().ReverseMap();
            CreateMap<GetAllPagedRegistrations_StudentResponse, Student>().ReverseMap();
            CreateMap<StdForReg, Student>().ReverseMap()
                .ForMember(d => d.CurrencyNo, opt => opt.MapFrom(x => x.Currency.Name))
                .ForMember(d => d.Semester, opt => opt.MapFrom(x => x.Semester.Name))
                .ForMember(d => d.StudentNumber, opt => opt.MapFrom(x => x.Id))
                .ForMember(d => d.StudentNameA, opt => opt.MapFrom(x => x.NameA))
                .ForMember(d => d.Facultynumber, opt => opt.MapFrom(x => x.Fuculty.NameA));
        }
        
    }
}