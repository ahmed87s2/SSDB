using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Specialization: AuditableEntity<int>
    {
        public string NameA { get; set; }
        public string NameE { get; set; }
        public int UniversityId { get; set; }
    }
}