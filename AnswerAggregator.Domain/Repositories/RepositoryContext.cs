using System.Data.Entity;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Enviroment.Interfaces;
using AnswerAggregator.Domain.Repositories.Interfaces;

namespace AnswerAggregator.Domain.Repositories
{
    public class RepositoryContext : IUnitOfWork
    {
        protected readonly ApplicationContext Context;

        //private readonly ILogger _logger;

        public RepositoryContext(ApplicationContext context)
        {
            Context = context;
            //_logger = logger;

            //Context.Database.Log = str => _logger.Log(GetType().Name, str);
        }

        public IRepository<T> GetRepository<T>() 
            where T : class
        {
            return new RepositoryBase<T>(Context);
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
