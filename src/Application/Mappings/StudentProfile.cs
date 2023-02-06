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
            CreateMap<GetAllPagedStudentsResponse, Student>().ReverseMap()
                .ForMember(d => d.NameA, opt => opt.MapFrom(s => $"{s.FirstNameA} {s.SecondNameA} {s.ThirdNameA} {s.FourthNameA}"));
            CreateMap<GetStudentByIdResponse, Student>().ReverseMap();
            CreateMap<GetRegistrationById_StudentResponse, Student>().ReverseMap()
                .ForMember(d => d.NameA, opt => opt.MapFrom(s => $"{s.FirstNameA} {s.SecondNameA} {s.ThirdNameA} {s.FourthNameA}")); 
            CreateMap<StudentsRegistrationInfo, Student>().ReverseMap()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.FirstNameA} {s.SecondNameA} {s.ThirdNameA} {s.FourthNameA}"))
                .ForMember(d => d.Semester, opt => opt.MapFrom(s => s.Semester.Name))
                .ForMember(d => d.CurrencyName, opt => opt.MapFrom(s => s.Currency.Name))
                .ForMember(d => d.Note, opt => opt.MapFrom(s => s.Comments))
                .ForMember(d => d.FucultyName, opt => opt.MapFrom(s => s.Fuculty.NameA));

            CreateMap<PaginatedResult<GetAllPagedStudentsResponse>, PaginatedResult<Student>>().ReverseMap();
            CreateMap<GetAllPagedRegistrations_StudentResponse, Student>().ReverseMap()
                .ForMember(d => d.NameA, opt => opt.MapFrom(s => $"{s.FirstNameA} {s.SecondNameA} {s.ThirdNameA} {s.FourthNameA}")); 
            CreateMap<StdForReg, Student>().ReverseMap()
                .ForMember(d => d.CurrencyNo, opt => opt.MapFrom(x => x.Currency.Name))
                .ForMember(d => d.Semester, opt => opt.MapFrom(x => x.Semester.Name))
                .ForMember(d => d.StudentNumber, opt => opt.MapFrom(x => x.Id))
                .ForMember(d => d.StudentNameA, opt => opt.MapFrom(x => x.FirstNameA))
                .ForMember(d => d.Facultynumber, opt => opt.MapFrom(x => x.Fuculty.NameA));

            CreateMap<GetStudentRegistrationInfoByIdResponse, Student>().ReverseMap()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.FirstNameA} {s.SecondNameA} {s.ThirdNameA} {s.FourthNameA}"))
                .ForMember(d => d.Batch, opt => opt.MapFrom(x => x.Batch.Name))
                .ForMember(d => d.College, opt => opt.MapFrom(x => x.Fuculty.NameA))
                .ForMember(d => d.Reg_fees, opt => opt.MapFrom(x => x.RegistrationFees))
                .ForMember(d => d.Section, opt => opt.MapFrom(x => x.Department.NameA))
                .ForMember(d => d.Term, opt => opt.MapFrom(x => x.Semester.Name))
                .ForMember(d => d.Panalty, opt => opt.MapFrom(x => x.Panalty))
                .ForMember(d => d.Student_no, opt => opt.MapFrom(x => x.Id))
                .ForMember(d => d.Currency, opt => opt.MapFrom(x => x.Currency.Name))
                .ForMember(d => d.Title, opt => opt.MapFrom(x => x.Specialization.NameA))
                .ForMember(d => d.Total_amount, opt => opt.MapFrom(x => x.StudyFees + x.MedicalFees + x.RegistrationFees))
                ;
        }

    }
}