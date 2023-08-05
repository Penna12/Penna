using System.Threading.Tasks;

namespace Penna.Core.Utilities.EmailService.Microsoft
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string content);
    }
}
