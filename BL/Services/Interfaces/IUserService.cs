using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        Task<OperationResult> CreateUser(UserDTO user);
        Task<UserDTO> GetUser(Guid id);
        Task<UserDTO> GetUser(string loginOrEmail, string password); 
        Task<OperationResult> SendConfirmationMessage(string code, string returnUrl);
        Task<Guid?> CreateVerificationToken(Guid id);
        Task<Guid?> CreateVerificationToken(string loginOrEmail);
        Task<OperationResult> ConfirmAccount(Guid token);
        Task<bool> CheckLoginOccuped(string login);
        Task<bool> CheckEmailOccuped(string email);
    }
}
