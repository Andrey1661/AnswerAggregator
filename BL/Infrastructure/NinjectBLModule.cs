using System.Data.Entity;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Enviroment;
using AnswerAggregator.Domain.Enviroment.Interfaces;
using AnswerAggregator.Domain.Repositories;
using AnswerAggregator.Domain.Repositories.Interfaces;
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
        private readonly string _serverFilesPath;
        private readonly string _logsRootFolder;

        public NinjectBLModule(string connectionString, string emailAddress, string password, string serverFilesPath, string logsRootFolder)
        {
            _connectionString = connectionString;
            _emailAddress = emailAddress;
            _password = password;
            _serverFilesPath = serverFilesPath;
            _logsRootFolder = logsRootFolder;

            Database.SetInitializer(new TestDbInitializer());
        }

        public override void Load()
        {
            if (Kernel == null) return;

            Kernel.Bind<ISubjectService>().To<SubjectService>();
            Kernel.Bind<ITopicService>().To<TopicService>();
            Kernel.Bind<IRegistrationDataService>().To<RegistrationDataService>();
            Kernel.Bind<IProfileService>().To<ProfileService>();

            Kernel.Bind<IFileService>().To<FileService>();
            Kernel.Bind<IFileManager>().To<ServerFileManager>().WithConstructorArgument(_serverFilesPath);

            Kernel.Bind<IMessageManager>().To<EmailMessageManager>();

            Kernel.Bind<IMessageSender>()
                .To<EmailMessageSender>()
                .WithConstructorArgument("emailAddress", _emailAddress)
                .WithConstructorArgument("password", _password);

            Kernel.Bind<ILogger>().To<FileLogger>().WithConstructorArgument(_logsRootFolder);
            Kernel.Bind<IUnitOfWork>().To<RepositoryContext>();
            Kernel.Bind<ApplicationContext>().ToSelf().WithConstructorArgument(_connectionString);
            Kernel.Bind<DbContext>().To<ApplicationContext>().WithConstructorArgument(_connectionString);
        }
    }
}
