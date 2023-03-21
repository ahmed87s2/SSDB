using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class Program : AuditableEntity<int>
    {
        public string NameE { get; set; }
        public string NameA { get; set; }
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}