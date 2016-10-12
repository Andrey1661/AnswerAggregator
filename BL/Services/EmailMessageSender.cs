using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BL.Services.Interfaces;

namespace BL.Services
{
    class EmailMessageSender : IMessageSender
    {
        private readonly string _emailAddress;
        private readonly string _password;

        public EmailMessageSender(string emailAddress, string password)
        {
            _emailAddress = emailAddress;
            _password = password;
        }

        public void SendMessage(string code, string message, string additionalInfo)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageAsync(string code, string message, string additionalInfo)
        {
            var mailMessage = new MailMessage(_emailAddress, code) { Subject = additionalInfo };
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            var credentials = new NetworkCredential(_emailAddress, _password);

            var smtp = new SmtpClient("smtp.gmail.com", 587) {EnableSsl = true, Credentials = credentials};

            await smtp.SendMailAsync(mailMessage);
        }
    }
}
