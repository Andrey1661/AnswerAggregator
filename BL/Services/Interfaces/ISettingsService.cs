using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Enviroment;

namespace BL.Services.Interfaces
{
    public interface ISettingsService
    {
        Task<OperationResult> ChangeSetting(string settingName, object value);
    }
}
