using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class EmailMessageManager : IMessageManager
    {
        protected IMessageSender Sender;

        public EmailMessageManager(IMessageSender sender)
        {
            Sender = sender;
        }

        public async Task SendConfirmationMessage(string code, string returnUrl)
        {
            string message = @"Для завершения регистрации перейдите по ссылке, указанной ниже<br/>";
            string linkText = "Ссылка для подтверждения";
            string link = string.Format("<a href='{0}'>{1}</a>", returnUrl, linkText);

            string block = string.Format("<div>{0} {1}</div>", message, link);
            string subject = "Answer Aggregator - Завершение регистрации";

            await Sender.SendMessageAsync(code, block, subject);
        }

        public async Task SendPasswordResetMessage(string code, string returnUrl)
        {
            string message = @"Для смены пароля перейжите по указанной ниже ссылке";
            string linkText = "Ссылка для смены пароля";
            string link = string.Format("<a href='{0}'>{1}</a>", returnUrl, linkText);

            string block = string.Format("<div>{0} {1}</div>", message, link);
            string subject = "Answer Aggregator - Восстановление пароля";

            await Sender.SendMessageAsync(code, block, subject);
        }
    }
}
