using System.Threading.Tasks;
using BL.DTO;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface IProfileService
    {
        Task<OperationResult> SetAvatar(string userName, FileModel file);

        Task<ProfileDto> GetProfile(string userName);
        Task<SettingsDto> GetSettings(string userName);
    }
}
