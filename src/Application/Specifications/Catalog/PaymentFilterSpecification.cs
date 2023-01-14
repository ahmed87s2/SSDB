using SSDB.Application.Specifications.Base;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Specifications.Catalog
{
    public class PaymentFilterSpecification : HeroSpecification<Payment>
    {
        public PaymentFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                double.TryParse(searchString, out double fees);
                Criteria = p => p.StudentNumber != default && (p.Name.Contains(searchString) || 
                p.Semester.Contains(searchString) || 
                p.FucultyName.Contains(searchString) || 
                p.PaymentNo.Contains(searchString) || 
                p.RegistrationFees == fees);
            }
            else
            {
                Criteria = p => p.StudentNumber != default;
            }
        }
    }
}