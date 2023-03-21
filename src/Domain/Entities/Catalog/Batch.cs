using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Batch : AuditableEntity<int>
    {
        public string Name { get; set; }
        public decimal RegistrationFees { get; set; }
        public decimal? StudyFees { get; set; }
        public int? ProgramId { get; set; }
        public Program Program { get; set; }

    }
}