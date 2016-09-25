using System;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IRepository<T> : IReadOnlyRepositoryAsync<T> where T : BaseEntity
    {
        Task Insert(T item);
        Task Update(T item);
        Task Delete(T item);
        Task DeleteAsync(Guid id);
    }
}
