using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class University : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}