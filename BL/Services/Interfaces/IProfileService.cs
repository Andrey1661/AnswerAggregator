using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetProfile(string userName);
        Task<SettingsDTO> GetSettings(string userName);
    }
}
