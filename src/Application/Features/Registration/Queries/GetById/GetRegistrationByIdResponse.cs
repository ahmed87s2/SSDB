using SSDB.Domain.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace SSDB.Application.Features.Registrations.Queries
{
    public class GetRegistrationByIdResponse
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public int SemesterId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public double Fees { get; set; }
        public double StudyFees { get; set; }
        public bool NoStudyFees { get; set; }
        public string AddmissionNo { get; set; }
        public int CurrencyId { get; set; }
        public string PaymentNo { get; set; }
        public int BranchId { get; set; }
        public int linkNo { get; set; }
        public string Comments { get; set; }
        public GetRegistrationById_StudentResponse Student { get; set; }
        public Semester Semester { get; set; }
        public Currency Currency { get; set; }
    }
    public class GetRegistrationById_StudentResponse
    {
        public int Id { get; set; }
        public string NameA { get; set; }
        public string Phone { get; set; }
        public bool NoStudyFees { get; set; }
    }
}