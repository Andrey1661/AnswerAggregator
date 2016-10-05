using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task CreateUser(UserDTO user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDTO> GetUser(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserDTO> GetUser(string loginOrEmail, string password); 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<UserDTO> GetUser(Expression<Func<UserDTO, bool>> predicate);

        Task SendConfirmationMessage(string code);

        Task<Guid> CreateVerificationToken(Guid id);

        Task ConfirmAccount(Guid token);
    }
}
