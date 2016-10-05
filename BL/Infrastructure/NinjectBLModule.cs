using System.Data.Entity;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Enviroment;
using AnswerAggregator.Domain.Enviroment.Interfaces;
using AnswerAggregator.Domain.Repositories;
using AnswerAggregator.Domain.Repositories.Interfaces;
using Ninject.Modules;

namespace BL.Infrastructure
{
    // ReSharper disable once InconsistentNaming
    public class NinjectBLModule : NinjectModule
    {
        private readonly string _connectionString;

        public NinjectBLModule(string connectionString)
        {
            Database.SetInitializer(new TestDbInitializer());

            _connectionString = connectionString;
        }

        public override void Load()
        {
            Kernel.Bind<IUnitOfWork>().To<RepositoryContext>();
            Kernel.Bind<ILogger>().To<ConsoleLogger>();
            Kernel.Bind<ApplicationContext>().ToSelf().WithConstructorArgument(_connectionString);
            Kernel.Bind<DbContext>().To<ApplicationContext>().WithConstructorArgument(_connectionString);
        }
    }
}
