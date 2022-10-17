using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Department : AuditableEntity<int>
    {
        public int FacultyId { get; set; }
        public string Number { get; set; }
        public string NameA { get; set; }
        public string NameE { get; set; }
        public Fuculty Fuculty { get; set; }
    }
}