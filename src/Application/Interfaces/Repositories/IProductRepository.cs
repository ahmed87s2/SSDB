using System.Threading.Tasks;

namespace SSDB.Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<bool> IsUniversityUsed(int studentId);
    }
}