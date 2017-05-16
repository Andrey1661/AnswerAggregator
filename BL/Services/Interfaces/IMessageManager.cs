using System;
using System.Threading.Tasks;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IMessageManager
    {
        Task SendConfirmationMessage(string code, string confirmationData);
        Task SendPasswordResetMessage(string code, string confirmationData);
    }
}
