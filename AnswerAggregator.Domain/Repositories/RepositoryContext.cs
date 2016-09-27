using System.Data.Entity;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Repositories.Interfaces;

namespace AnswerAggregator.Domain.Repositories
{
    public class RepositoryContext : IUnitOfWork
    {
        protected readonly DbContext Context;

        public RepositoryContext(DbContext context)
        {
            Context = context;
        }

        public virtual IRepository<T> Repository<T>() where T : class 
        {
            return new RepositoryBase<T>(Context);
        }

        public async Task SaveChangesAsync()
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
