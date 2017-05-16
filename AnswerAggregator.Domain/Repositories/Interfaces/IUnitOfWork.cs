using System;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;

        void Update<T>(T item) where T : class;
        void Save();
        Task SaveAsync();
    }
}
