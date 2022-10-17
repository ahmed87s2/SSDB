using SSDB.Application.Requests.Mail;
using System.Threading.Tasks;

namespace SSDB.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}