using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public RepositoryBase(DbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        } 


        public async Task<T> Get(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await Set.FirstOrDefaultAsync(predicate);
        }


        public async Task<IEnumerable<T>> GetAll(string includeProperties = null)
        {
            return await Set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate, string includeProperties = null)
        {
            return await Set.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, 
            bool descending = false, string includeProperties = null)
        {
            var result = Set.Where(predicate);

            if (descending)
            {
                return await result.OrderByDescending(orderBy).ToListAsync();
            }
            return await result.OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetList<TKey>(Expression<Func<T, bool>> predicate, Expression<Func<T, TKey>> orderBy, 
            int skip, int take, bool descending = false, string includeProperties = null)
        {
            var result = Set.Where(predicate);

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
