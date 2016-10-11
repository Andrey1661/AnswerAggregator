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

        Task<UserDTO> GetUser(Expression<Func<UserDTO, bool>> predicate);

        Task<OperationResult> SendConfirmationMessage(string code);

        Task<Guid?> CreateVerificationToken(Guid id);

        Task<OperationResult> ConfirmAccount(Guid token);
    }
}
