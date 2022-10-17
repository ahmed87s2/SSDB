using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Nationality : AuditableEntity<int>
    {
        public string NameA { get; set; }
        public string NameE { get; set; }
    }
}