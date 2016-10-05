using System;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserProfile> UserProfiles { get; }
        IRepository<UserIdentity> UserIdentities { get; }
        IRepository<UserSettings> UserSettings { get; }
        IRepository<UserMessage> UserMessages { get; }
        IRepository<Department> Departments { get; }
        IRepository<Group> Groups { get; }
        IRepository<Subject> Subjects { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<GroupSubject> GroupSubjects { get; }
        IRepository<Institute> Institutes { get; }
        IRepository<University> Universities { get; }
        IRepository<Topic> Topics { get; }
        IRepository<Post> Posts { get; }

        void Save();
        Task SaveAsync();
    }
}
