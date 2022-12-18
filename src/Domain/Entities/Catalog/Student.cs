using SSDB.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSDB.Domain.Entities.Catalog
{
    public class Student : AuditableEntity<string>
    {
        public string NameA { get; set; }
        public string NameE { get; set; }
        public int BatchId { get; set; }
        public int FucultyId { get; set; }
        public int DepartmentId { get; set; }
        public int ProgramId { get; set; }
        public int AddmissionId { get; set; }
        public string AddmissionFormNo { get; set; }
        public int First_semster { get; set; }
        public string Phone { get; set; }
        public int NationalityId { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string CertificateType { get; set; }
        public string Std_Picture { get; set; }
        public int SpecializationId { get; set; }
        public DateTime? GraduationDate { get; set; }
        public decimal? MedicalFees { get; set; }
        public int? StdPassword { get; set; }
        public bool NoStudyFees { get; set; }
        public int CurrencyId { get; set; }
        public string Comments { get; set; }
        public decimal? AdvisorId { get; set; }
        public string Record_Status { get; set; }
        public string RegType { get; set; }
        public decimal? CGPA { get; set; }
        public string Status { get; set; }
        public int SemesterId { get; set; }
        public decimal? ToLocalCurrency { get; set; }
        public decimal StudyFeesUpdated { get; set; }
        public decimal RegistrationFees { get; set; }
        public int Panalty { get; set; }
        public University University { get; set; }
        public Addmission Addmission { get; set; }
        public Department Department { get; set; }
        public Batch Batch { get; set; }
        public Currency Currency { get; set; }
        public Fuculty Fuculty { get; set; }
        public Program Program { get; set; }
        public Specialization Specialization { get; set; }
        public Semester Semester { get; set; }
    }
}