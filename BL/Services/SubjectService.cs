using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories;
using AnswerAggregator.Domain.Repositories.Interfaces;
using BL.DTO;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class SubjectService : ServiceBase, ISubjectService
    {
        protected readonly IRepository<Subject> Subjects;
        protected readonly IRepository<GroupSubject> GroupSubjects;
        protected readonly IRepository<Group> Groups;

        public SubjectService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Subjects = UnitOfWork.GetRepository<Subject>();
            GroupSubjects = UnitOfWork.GetRepository<GroupSubject>();
            Groups = UnitOfWork.GetRepository<Group>();
        }

        public async Task<IEnumerable<SubjectIdentity>> GetSubjects(Guid groupId, int semester)
        {
            var subjects =
                await Subjects.GetList(
                    s => s.GroupSubjects.Where(gs => gs.SemesterNumber == semester)
                        .Select(gs => gs.GroupId)
                        .Contains(groupId));

            var model = subjects.Select(t => new SubjectIdentity
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            return model;
        }
    }
}
