using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IStudyDataService
    {
        Task<IEnumerable<string>> GetUniversities();
        Task<IEnumerable<string>> GetInstitutes(string universityName);
        Task<IEnumerable<string>> GetGroups(string instituteName, int course);
    }
}
