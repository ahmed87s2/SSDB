using SSDB.Application.Specifications.Base;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Application.Specifications.Catalog
{
    public class StudentFilterSpecification : HeroSpecification<Student>
    {
        public StudentFilterSpecification(string searchString)
        {
            Includes.Add(a => a.University);
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Id != null && (p.NameA.Contains(searchString) || p.Comments.Contains(searchString) || p.Batch.Name.Contains(searchString) || p.University.Name.Contains(searchString));
            }
            else
            {
                Criteria = p => p.Id != null;
            }

        }
    } 
}