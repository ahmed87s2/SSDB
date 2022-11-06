using SSDB.Domain.Entities.Catalog;
using System;
using System.ComponentModel.DataAnnotations;

namespace SSDB.Application.Features.Registrations.Queries
{
    public class GetAllPagedRegistrationsResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public double Fees { get; set; }
        public double StudyFees { get; set; }
        public string PaymentNo { get; set; }
        public int BranchId { get; set; }
        public int linkNo { get; set; }
        public GetAllPagedRegistrations_StudentResponse Student { get; set; }
        public Semester Semester { get; set; }
        public Currency Currency { get; set; }
    }
    public class GetAllPagedRegistrations_StudentResponse
    {
        public string Id { get; set; }
        public string NameA { get; set; }
        public string Phone { get; set; }
        public bool NoStudyFees { get; set; }
    }
}