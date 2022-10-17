using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;

namespace SSDB.Infrastructure.Repositories
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly IRepositoryAsync<University, int> _repository;

        public UniversityRepository(IRepositoryAsync<University, int> repository)
        {
            _repository = repository;
        }
    }
}