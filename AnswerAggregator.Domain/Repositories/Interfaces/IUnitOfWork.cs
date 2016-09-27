using System;
using System.Threading.Tasks;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        Task SaveChangesAsync();
    }
}
