using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BL.Services.Interfaces;

namespace BL.Services
{
    internal class EmailMessageSender : IMessageSender
    {
        private readonly string _emailAddress;
        private readonly string _password;

        public EmailMessageSender(string emailAddress, string password)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException("emailAddress", "Не передан электронный адрес отправителя");
            }

            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                throw new ArgumentNullException("password", "Не передан пароль отправителя");
            }

            _emailAddress = emailAddress;
            _password = password;
        }

        public void SendMessage(string code, string message, string additionalInfo)
        {
            var mailMessage = CreateMailMessage(code, message, additionalInfo);
            var smtp = CreateSmtpClient();

            smtp.Send(mailMessage);
        }

        public async Task SendMessageAsync(string code, string message, string additionalInfo)
        {
            var mailMessage = CreateMailMessage(code, message, additionalInfo);
            var smtp = CreateSmtpClient();

            await smtp.SendMailAsync(mailMessage);
        }

        private MailMessage CreateMailMessage(string code, string message, string additionalInfo)
        {
            return new MailMessage(_emailAddress, code)
            {
                Subject = additionalInfo,
                IsBodyHtml = true,
                Body = message
            };
        }

        private SmtpClient CreateSmtpClient()
        {
            var credentials = new NetworkCredential(_emailAddress, _password);
            return new SmtpClient("smtp.gmail.com", 587) { EnableSsl = true, Credentials = credentials };
        }
    }
}
