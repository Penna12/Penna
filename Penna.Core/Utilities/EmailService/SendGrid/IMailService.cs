using System.Threading.Tasks;

namespace Penna.Core.Utilities.EmailService.SendGrid
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
