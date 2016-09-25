using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IReadOnlyRepository<T> : IDisposable where T : BaseEntity
    {
        T Get(Guid id);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);
        IEnumerable<TResult> GetList<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector);
        int Count(Expression<Func<T, bool>> predicate);
    }
}
