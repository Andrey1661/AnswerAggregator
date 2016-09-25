using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;

namespace AnswerAggregator.Domain.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected DbContext Context;
        protected readonly DbSet<T> Set;

        public RepositoryBase(DbContext context)
        {
            Context = context;
            Set = context.Set<T>();
        }

        public async Task Insert(T item)
        {
            Set.Add(item);
            await Context.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task Delete(T item)
        {
            if (item != null)
            {
                Set.Remove(item);
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await Set.FindAsync(id);

            if (item != null) Set.Remove(item);

            await Context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TResult>> GetListAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector)
        {
            return await Set.Where(predicate).Select(selector).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.CountAsync();
        }

        public void Dispose()
        {
            
        }
    }
}
