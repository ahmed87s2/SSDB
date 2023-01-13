using SSDB.Domain.Contracts;

namespace SSDB.Domain.Entities.Catalog
{
    public class UniversityConfigs : AuditableEntity<int>
    {
        public int UniversityId { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public string Body { get; set; }
    }
}