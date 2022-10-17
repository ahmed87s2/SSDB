using SSDB.Application.Specifications.Base;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Specifications.Catalog
{
    public class UniversityFilterSpecification : HeroSpecification<University>
    {
        public UniversityFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
