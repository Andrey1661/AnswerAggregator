using System;
using AnswerAggregator.Domain.Repositories;
using AnswerAggregator.Domain.Repositories.Interfaces;
using Ninject.Modules;

namespace BL.Infrastructure
{
    // ReSharper disable once InconsistentNaming
    public class NinjectBLModule : NinjectModule
    {
        private string _connectionString;

        public NinjectBLModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Kernel.Bind<IUnitOfWork>().To<RepositoryContext>();
        }
    }
}
