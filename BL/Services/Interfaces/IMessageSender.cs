using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IMessageSender
    {
        void SendMessage(string code, string message, string additionalInfo);
        Task SendMessageAsync(string code, string message, string additionalInfo);
    }
}
