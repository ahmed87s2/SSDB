using SSDB.Domain.Contracts;
using System;

namespace SSDB.Domain.Entities.Catalog
{
    public class Payment : AuditableEntity<int>
    {
        public DateTime Date { get; set; }
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public double RegistrationFees { get; set; }
        public string CurrencyName { get; set; }
        public bool NoStudyFees { get; set; }
        public string Note { get; set; }
        public string FucultyName { get; set; }
        public string Semester { get; set; }
        public string PaymentNo { get; set; }
        public string Status { get; set; }
    }
}