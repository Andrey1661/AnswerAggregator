using System;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;
using AutoMapper;
using BL.DTO;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class ProfileService : ServiceBase, IProfileService
    {
        protected const string UserFilesPath = "Users";

        protected readonly IFileManager FileManager;

        protected readonly IRepository<UserProfile> Users;
        protected readonly IRepository<UserIdentity> Identities;
        protected readonly IRepository<UserSettings> UserSettings;

        public ProfileService(IUnitOfWork unitOfWork, IFileManager fileManager)
            : base(unitOfWork)
        {
            Users = UnitOfWork.GetRepository<UserProfile>();
            Identities = UnitOfWork.GetRepository<UserIdentity>();
            UserSettings = UnitOfWork.GetRepository<UserSettings>();

            FileManager = fileManager;
        }

        public async Task<OperationResult> SetAvatar(string userName, FileModel file)
        {
            return await FileManager.SetUserAvatarAsync(userName, file);
        }

        public async Task<ProfileDto> GetProfile(string email)
        {
            var user = await Users.Get(t => t.Email == email);

            var profile = Mapper.Map<ProfileDto>(user);
            profile.Avatar = FileManager.GetAvatar(email);

            return profile;
        }

        public async Task<SettingsDto> GetSettings(string userName)
        {
            throw new NotImplementedException();
        }


    }
}
