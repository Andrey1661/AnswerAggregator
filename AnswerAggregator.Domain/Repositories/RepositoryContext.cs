using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Enviroment.Interfaces;
using AnswerAggregator.Domain.Repositories.Interfaces;

namespace AnswerAggregator.Domain.Repositories
{
    public class RepositoryContext : IUnitOfWork
    {
        protected readonly ApplicationContext Context;

        private readonly ILogger _logger;

        #region //repositories

        private IRepository<UserProfile> _userProfiles;
        private IRepository<UserIdentity> _userIdentities;
        private IRepository<UserSettings> _userSettings;
        private IRepository<UserMessage> _userMessages;
        private IRepository<Department> _departments;
        private IRepository<Group> _groups;
        private IRepository<Subject> _subjects;
        private IRepository<Teacher> _teachers;
        private IRepository<GroupSubject> _groupSubjects;
        private IRepository<Institute> _institutes;
        private IRepository<University> _universities;
        private IRepository<Topic> _topics;
        private IRepository<Post> _posts;

        public IRepository<UserProfile> UserProfiles
        {
            get { return _userProfiles ?? (_userProfiles = new RepositoryBase<UserProfile>(Context)); }
        }
        public IRepository<UserIdentity> UserIdentities
        {
            get { return _userIdentities ?? (_userIdentities = new RepositoryBase<UserIdentity>(Context)); }
        }
        public IRepository<UserSettings> UserSettings
        {
            get { return _userSettings ?? (_userSettings = new RepositoryBase<UserSettings>(Context)); }
        }
        public IRepository<UserMessage> UserMessages
        {
            get { return _userMessages ?? (_userMessages = new RepositoryBase<UserMessage>(Context)); }
        }
        public IRepository<Department> Departments
        {
            get { return _departments ?? (_departments = new RepositoryBase<Department>(Context)); }
        }
        public IRepository<Group> Groups
        {
            get { return _groups ?? (_groups = new RepositoryBase<Group>(Context)); }
        }
        public IRepository<Subject> Subjects
        {
            get { return _subjects ?? (_subjects = new RepositoryBase<Subject>(Context)); }
        }
        public IRepository<Teacher> Teachers
        {
            get { return _teachers ?? (_teachers = new RepositoryBase<Teacher>(Context)); }
        }
        public IRepository<GroupSubject> GroupSubjects
        {
            get { return _groupSubjects ?? (_groupSubjects = new RepositoryBase<GroupSubject>(Context)); }
        }
        public IRepository<Institute> Institutes
        {
            get { return _institutes ?? (_institutes = new RepositoryBase<Institute>(Context)); }
        }
        public IRepository<University> Universities
        {
            get { return _universities ?? (_universities = new RepositoryBase<University>(Context)); }
        }
        public IRepository<Topic> Topics
        {
            get { return _topics ?? (_topics = new RepositoryBase<Topic>(Context)); }
        }
        public IRepository<Post> Posts
        {
            get { return _posts ?? (_posts = new RepositoryBase<Post>(Context)); }
        }

        #endregion

        public RepositoryContext(ApplicationContext context, ILogger logger)
        {
            Context = context;
            _logger = logger;

            Context.Database.Log = str => _logger.Log(GetType().Name, str);
        }

        public void Update<T>(T item) where T : class
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}
