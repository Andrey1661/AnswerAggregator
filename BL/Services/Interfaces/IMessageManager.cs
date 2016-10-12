using System;
using System.Threading.Tasks;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IMessageManager
    {
        Task SendEmailConfirmationMessage(string code, string returnUrl);
    }
}
