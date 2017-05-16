using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface IRegistrationDataService
    {
        Task<IEnumerable<string>> GetUniversities();
        Task<IEnumerable<string>> GetInstitutes(string universityName);
        Task<IEnumerable<GroupIdentity>> GetGroups(string instituteName, int course);
    }
}
