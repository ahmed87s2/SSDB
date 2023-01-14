using SSDB.Domain.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSDB.Domain.Entities.Catalog
{
    public class Registration : AuditableEntity<int>
    {
        public string StudentId { get; set; }
        public int SemesterId { get; set; }
        public int Status { get; set; }
        public double Fees { get; set; }
        public double StudyFees { get; set; }
        public bool NoStudyFees { get; set; }
        public int CurrencyId { get; set; }
        public string PaymentNo { get; set; }
        public int BranchId { get; set; }
        public int UniversityId { get; set; }
        public int linkNo { get; set; }
        public string Comments { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        public Semester Semester { get; set; }
        public Currency Currency { get; set; }
        public University University { get; set; }


    }
}