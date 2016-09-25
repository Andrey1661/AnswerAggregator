using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IReadOnlyRepositoryAsync<T> : IDisposable where T : BaseEntity 
    {
        Task<T> GetAsync(Guid id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
