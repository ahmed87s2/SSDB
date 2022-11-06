using SSDB.Application.Specifications.Base;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Specifications.Catalog
{
    public class RegistrationFilterSpecification : HeroSpecification<Registration>
    {
        public RegistrationFilterSpecification(string searchString)
        {
            Includes.Add(a => a.Student);
            if (!string.IsNullOrEmpty(searchString))
            {
                double.TryParse(searchString, out double fees);
                Criteria = p => p.StudentId != default && (p.Student.NameA.Contains(searchString) || 
                p.Student.NameE.Contains(searchString) || 
                p.Semester.Name.Contains(searchString) || 
                p.PaymentNo.Contains(searchString) || 
                p.StudyFees == fees);
            }
            else
            {
                Criteria = p => p.StudentId != default;
            }
        }
    }
}