using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string code, string message, string additionalInfo);
        Task SendMessageAsync(string code, string message, string additionalInfo);
    }
}
