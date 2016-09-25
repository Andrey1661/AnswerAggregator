using System;
using AnswerAggregator.Domain.Entities;

namespace AnswerAggregator.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}
