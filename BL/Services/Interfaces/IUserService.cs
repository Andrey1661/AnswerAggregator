using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO user);
        Task<UserDTO> GetUser(string login);
    }
}
