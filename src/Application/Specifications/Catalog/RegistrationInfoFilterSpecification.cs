using SSDB.Application.Specifications.Base;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Specifications.Catalog
{
    public class RegistrationInfoFilterSpecification : HeroSpecification<StudentsRegistrationInfo>
    {
        public RegistrationInfoFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                double.TryParse(searchString, out double fees);
                Criteria = p => p.StudentNumber != default && 
                p.StudentNumber.Contains(searchString) ||
                p.Semester.Contains(searchString) ||
                p.PaymentNo.Contains(searchString) ||
                p.RegistrationFees == fees;
            }
            else
            {
                Criteria = p => p.StudentNumber != default;
            }
        }
    }
}