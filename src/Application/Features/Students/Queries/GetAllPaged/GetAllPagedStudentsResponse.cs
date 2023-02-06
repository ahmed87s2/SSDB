using SSDB.Domain.Contracts;
using SSDB.Domain.Entities.Catalog;
using System.ComponentModel.DataAnnotations;
using System;

namespace SSDB.Application.Features.Students.Queries
{
    public class GetAllPagedStudentsResponse
    {
        public string Id { get; set; }
        public int First_semster { get; set; }
        public string Phone { get; set; }
        public decimal MedicalFees { get; set; }
        public string NameA { get; set; }
        public string Status { get; set; }
        public string AddmissionNo { get; set; }
        public DateTime GraduationDate { get; set; }
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