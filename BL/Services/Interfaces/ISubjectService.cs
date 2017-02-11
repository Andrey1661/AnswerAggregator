using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectIdentity>> GetSubjects(Guid groupId, int semester);
    }
}
