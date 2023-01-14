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
        public ICollection<Batch> Batches { get; set; }
        public ICollection<Addmission> Addmissions { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<Fuculty> Fuculties { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Currency> Currencies { get; set; }
        public ICollection<StudentsRegistrationInfo> RegistrationInfo { get; set; }
        public ICollection<Program> Programs { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Semester> Semesters { get; set; }
        public ICollection<Specialization> Specializations { get; set; }
        public ICollection<Student> Students { get; set; }
        public UniversityConfigs Configs { get; set; }

    }
}