using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Semester : AuditableEntity<int>
    {
        public string Name { get; set; }
        public int ProgramId { get; set; }
        public double MaxHours { get; set; }
        public int? BatchId { get; set; }
        public Batch Batch { get; set; }
    }
}