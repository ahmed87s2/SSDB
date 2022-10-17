using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Currency : AuditableEntity<int>
    {
        public string Name { get; set; }
        public double Adjust { get; set; }
    }
}