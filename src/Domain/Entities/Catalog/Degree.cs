using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Degree : AuditableEntity<int>
    {
        public string Name { get; set; }
        public int UniversityId { get; set; }
    }
}