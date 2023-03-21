using SSDB.Domain.Contracts;
using System.Collections;
using System.Collections.Generic;

namespace SSDB.Domain.Entities.Catalog
{
    public class University : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public int UniversityConfigsId { get; set; }
        public UniversityConfigs Configs { get; set; }

    }
}