using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IMessageSender
    {
        void SendMessage(string code, string message, string additionalInfo);
        Task SendMessageAsync(string code, string message, string additionalInfo);
    }
}
