using Microsoft.Extensions.Configuration;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Penna.Core.Utilities.EmailService.Microsoft
{
    public class EmailHelper : IMailService
    {
        private readonly IConfiguration _configuration;
        private EmailConfiguration emailConfiguration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            emailConfiguration = _configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string content)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(emailConfiguration.From);
            mailMessage.To.Add(new MailAddress(toEmail));

            mailMessage.Subject = subject;// "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = content;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(emailConfiguration.Username, emailConfiguration.Password);
            client.Host = emailConfiguration.SmtpServer;
            client.Port = emailConfiguration.Port;
            
            try
            {
                await client.SendMailAsync(mailMessage);
                mailMessage.Dispose();
                client.Dispose();
                return true;
            }
            catch (Exception )
            {
                // log exception
                //Logs.Log.writeLog($"{DateTime.Now} => Mail gönderim hatası: {ex.Message},  InnerException: {ex.InnerException.Message}");
            }
            mailMessage.Dispose();
            client.Dispose();
            //Logs.Log.writeLog($"Mail From: {emailConfiguration.From},  Username: {emailConfiguration.Username}, Password:{emailConfiguration.Password}, SmtpServer:{emailConfiguration.SmtpServer}, Port:{emailConfiguration.Port}");
            return false;
        }
    }
}

/* Kullanımı:
    var content = _userService.CreateRegisterUserContent(name, eposta, pass);
    var result = _mailService.SendEmailAsync(eposta, "Yeni Üyelik Bilgilendirme.", content).Result;
    if (result == true)
        return new SuccessResult("Kullanıcı bilgileri gönderildi.");
    else
        return new ErrorResult("Kullanıcı bilgileri gönderilemedi.");
 */