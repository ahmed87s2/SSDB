using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Currency : AuditableEntity<int>
    {
        public string Name { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}