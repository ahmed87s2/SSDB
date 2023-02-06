using SSDB.Domain.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace SSDB.Application.Features.Students.Queries
{
    public class GetStudentByIdResponse
    {
        public string Id { get; set; }
        [Required]
        public string FirstNameA { get; set; }
        [Required]
        public string FourthNameA { get; set; }
        [Required]
        public string SecondNameA { get; set; }
        [Required]
        public string ThirdNameA { get; set; }
        [Required]
        public string FirstNameE { get; set; }
        [Required]
        public string SecondNameE { get; set; }
        [Required]
        public string ThirdNameE { get; set; }
        [Required]
        public string FourthNameE { get; set; }
        public string IdentityNo { get; set; }
        public string AddmissionNo { get; set; }
        public string SeatNo { get; set; }
        public int DegreeId { get; set; }
        
        [Required]
        public int? BatchId { get; set; }
        public string Phone { get; set; }
        [Required]
        public decimal MedicalFees { get; set; }
        [Required]
        public decimal? StudyFees { get; set; }
        [Required]
        public int FucultyId { get; set; }
        public int DepartmentId { get; set; }
        public int ProgramId { get; set; }
        public int AddmissionId { get; set; }
        public string AddmissionFormNo { get; set; }
        public int First_semster { get; set; }
        public int NationalityId { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string CertificateType { get; set; }
        public string Std_Picture { get; set; }
        public int SpecializationId { get; set; }
        public DateTime GraduationDate { get; set; }
        public int RegistrationId { get; set; }
        public int StudentStatus { get; set; }
        public int StdPassword { get; set; }
        public int NoStudyFees { get; set; }
        public int CurrencyId { get; set; }
        public string Comments { get; set; }
        public decimal AdvisorId { get; set; }
        public string Record_Status { get; set; }
        public string RegType { get; set; }
        public decimal CGPA { get; set; }
        public string Status { get; set; }
        public int SemesterId { get; set; }
        public decimal ToLocalCurrency { get; set; }
        public decimal StudyFeesUpdated { get; set; }
        public Addmission Addmission { get; set; }
        public Department Department { get; set; }
        public Registration Registration { get; set; }
        public Batch Batch { get; set; }
        public Currency Currency { get; set; }
        public Fuculty Fuculty { get; set; }
        public Program Program { get; set; }
        public Specialization Specialization { get; set; }
        public Semester Semester { get; set; }
    }
}