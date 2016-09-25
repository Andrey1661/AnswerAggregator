using System.Data.Entity;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
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

        public virtual IRepository<T> Repository<T>() where T : BaseEntity
        {
            return new RepositoryBase<T>(Context);
        }

        public virtual void Dispose()
        {
            Context.Dispose();
        }
    }
}
