using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Repositories.Interfaces;

namespace AnswerAggregator.Domain.Repositories
{
    internal class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> Set;

        private IQueryable<T> _query; 

        public RepositoryBase(DbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
            _query = Set.AsQueryable();
        }


        public IRepository<T> Include(string property)
        {
            _query = _query.Include(property);

            return this;
        }

        public IRepository<T> Include<TResult>(Expression<Func<T, TResult>> propertyExpression)
        {
            _query = _query.Include(propertyExpression);

            return this;
        }

        public async Task<T> Get(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate)
        {
            return await _query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, 
            bool descending = false)
        {
            var result = _query.Where(predicate);

            if (descending)
            {
                return await result.OrderByDescending(orderBy).ToListAsync();
            }
            return await result.OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, 
            int skip, int take, bool descending = false)
        {
            var result = _query.Where(predicate);

            result = descending ? result.OrderByDescending(orderBy) : result.OrderBy(orderBy);

            return await result.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate = null)
        {
            return await Set.CountAsync(predicate);
        }

        public async Task<int> Average(Expression<Func<T, int>> selector)
        {
            return (int)await Set.AverageAsync(selector);
        }

        public async Task<int> Min(Expression<Func<T, int>> selector)
        {
            return await Set.MinAsync(selector);
        }

        public async Task<int> Max(Expression<Func<T, int>> selector)
        {
            return await Set.MaxAsync(selector);
        }


        public void Insert(T item)
        {
            Set.Add(item);
        }

        public void Delete(T item)
        {
            Set.Remove(item);
        }


        public void InsertRange(IEnumerable<T> items)
        {
            Set.AddRange(items);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            Set.RemoveRange(items);
        }
    }
}
