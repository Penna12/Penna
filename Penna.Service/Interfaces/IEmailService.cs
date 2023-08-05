using Penna.Entities.DTOs;
using System.Threading.Tasks;

namespace Penna.Business.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailMessage(EmailDto emailDto);
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);

        Task SendEmailForTemporaryPassword(UserEmailOptions userEmailOptions);
    }
}
