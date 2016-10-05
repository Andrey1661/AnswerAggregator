using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Interfaces;

namespace BL.Services
{
    class EmailMessageSender : IMessageSender
    {
        public void SendMessage(string code, string message, string additionalInfo)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageAsync(string code, string message, string additionalInfo)
        {
            throw new NotImplementedException();
        }
    }
}
