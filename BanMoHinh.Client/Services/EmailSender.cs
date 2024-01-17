using BanMoHinh.Client.Helper;
using BanMoHinh.Client.IServices;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace BanMoHinh.Client.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _emailSenderOptions;

        public EmailSender(IOptions<EmailSenderOptions> emailSenderOptions)
        {
            _emailSenderOptions = emailSenderOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(_emailSenderOptions.SmtpServer, _emailSenderOptions.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSenderOptions.Email, _emailSenderOptions.Password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage(_emailSenderOptions.Email, email, subject, htmlMessage)
            {
                IsBodyHtml = true,
            };

            return client.SendMailAsync(mailMessage);
        }
    }
}
