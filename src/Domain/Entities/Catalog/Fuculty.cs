using SSDB.Domain.Contracts;
using System.Collections.Generic;

namespace SSDB.Domain.Entities.Catalog
{
    public class Fuculty : AuditableEntity<int>
    {
        public string NameE { get; set; }
        public string NameA { get; set; }
        public string ShortName { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }
    }
}