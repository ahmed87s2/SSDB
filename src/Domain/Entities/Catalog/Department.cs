using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Department : AuditableEntity<int>
    {
        public string Number { get; set; }
        public string NameA { get; set; }
        public string NameE { get; set; }
        public int? FucultyId { get; set; }
        public Fuculty Fuculty { get; set; }
    }
}