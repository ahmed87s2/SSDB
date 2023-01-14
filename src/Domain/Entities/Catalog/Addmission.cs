using SSDB.Domain.Contracts;
using System;

namespace SSDB.Domain.Entities.Catalog
{
    public class Addmission: AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public int UniversityId { get; set; }
    }
}