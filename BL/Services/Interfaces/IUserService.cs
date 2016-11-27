using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationResult> CreateUser(UserDTO user);

        Task<UserLoginData> GetUserLoginData(Guid id);
        Task<UserLoginData> GetUserLoginData(string loginOrEmail);
        Task<UserLoginData> GetUserLoginData(string loginOrEmail, string password);

        Task<OperationResult> SendConfirmationMessage(string code, string returnUrl);
        Task<OperationResult> SendPasswordResetMessage(string code, string returnUrl);

        Task<Guid?> CreateVerificationToken(Guid id);
        Task<Guid?> CreateVerificationToken(string loginOrEmail);

        Task<Guid?> CreatePasswordResetToken(Guid id);
        Task<Guid?> CreatePasswordResetToken(string loginOrEmail);

        Task<OperationResult> CheckUserPasswordResetToken(Guid token);

        Task<OperationResult> ConfirmAccount(Guid token);

        Task<OperationResult> ResetUserPassword(Guid token, string newPassword);
            
        Task<bool> CheckLoginOccuped(string login);
        Task<bool> CheckEmailOccuped(string email);
    }
}
