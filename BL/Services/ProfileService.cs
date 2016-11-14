using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;
using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class ProfileService : ServiceBase, IProfileService
    {
        protected readonly IRepository<UserProfile> Users;
        protected readonly IRepository<UserIdentity> Identities;
        protected readonly IRepository<UserSettings> UserSettings; 

        public ProfileService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            Users = UnitOfWork.GetRepository<UserProfile>();
            Identities = UnitOfWork.GetRepository<UserIdentity>();
            UserSettings = UnitOfWork.GetRepository<UserSettings>();
        }

        public async Task<ProfileDTO> GetProfile(string email)
        {
            var user = await Users.Get(t => t.Email == email);

            var profile = new ProfileDTO
            {
                Email = user.Email,
                UserName = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                AccountVerified = user.Identity.AccountVerified
            };

            return profile;
        }

        public async Task<SettingsDTO> GetSettings(string userName)
        {
            var settings = await UserSettings.Get(t => t.Profile.Email == userName);

            return new SettingsDTO();
        }
    }
}
