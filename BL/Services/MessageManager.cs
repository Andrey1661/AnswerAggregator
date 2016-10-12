using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class MessageManager : IMessageManager
    {
        protected IMessageSender Sender;

        public MessageManager(IMessageSender sender)
        {
            Sender = sender;
        }

        public async Task SendEmailConfirmationMessage(string code, string returnUrl)
        {
            string message = @"Для завершения регистрации перейдите по ссылке, указанной ниже<br/>";
            string linkText = "Ссылка для подтверждения";
            string link = string.Format("<a href='{0}'>{1}</a>", returnUrl, linkText);

            string block = string.Format("<div>{0} {1}</div>", message, link);
            string subject = "Answer Aggregator - Завершение регистрации";

            await Sender.SendMessageAsync(code, block, subject);
        }
    }
}
