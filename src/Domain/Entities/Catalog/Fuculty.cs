using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Fuculty : AuditableEntity<int>
    {
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string ShortName { get; set; }
        public int OrderColumn { get; set; }
        public int OrderId { get; set; }
    }
}