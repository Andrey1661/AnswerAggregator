using System.Data.Entity;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Enviroment;
using AnswerAggregator.Domain.Enviroment.Interfaces;
using AnswerAggregator.Domain.Repositories;
using AnswerAggregator.Domain.Repositories.Interfaces;
using AutoMapper;
using BL.DTO;
using BL.Infrastructure.DbInitialization;
using BL.Services;
using BL.Services.Interfaces;
using Ninject.Modules;

namespace BL.Infrastructure
{
    // ReSharper disable once InconsistentNaming
    public class NinjectBLModule : NinjectModule
    {
        private readonly string _connectionString;
        private readonly string _emailAddress;
        private readonly string _password;

        public NinjectBLModule(string connectionString, string emailAddress, string password)
        {
            _connectionString = connectionString;
            _emailAddress = emailAddress;
            _password = password;

            Database.SetInitializer(new TestDbInitializer());

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserDTO, UserProfile>();
                cfg.CreateMap<UserProfile, UserDTO>();
            });
        }

        public override void Load()
        {
            if (Kernel == null) return;

            Kernel.Bind<IStudyDataService>().To<StudyDataService>();
            Kernel.Bind<IProfileService>().To<ProfileService>();
            Kernel.Bind<IMessageManager>().To<EmailMessageManager>();
            Kernel.Bind<IUnitOfWork>().To<RepositoryContext>();
            Kernel.Bind<IMessageSender>()
                .To<EmailMessageSender>()
                .WithConstructorArgument("emailAddress", _emailAddress)
                .WithConstructorArgument("password", _password);

            Kernel.Bind<ILogger>().To<ConsoleLogger>();
            Kernel.Bind<ApplicationContext>().ToSelf().WithConstructorArgument(_connectionString);
            Kernel.Bind<DbContext>().To<ApplicationContext>().WithConstructorArgument(_connectionString);
        }
    }
}
