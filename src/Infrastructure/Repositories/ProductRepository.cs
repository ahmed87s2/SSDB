using SSDB.Application.Interfaces.Repositories;
using SSDB.Domain.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SSDB.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IRepositoryAsync<Student, int> _repository;

        public StudentRepository(IRepositoryAsync<Student, int> repository)
        {
            _repository = repository;
        }

        public async Task<bool> IsUniversityUsed(int studentId)
        {
            return await _repository.Entities.AnyAsync(b => b.AddmissionId == studentId);
        }
    }
}