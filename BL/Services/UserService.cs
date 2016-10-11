using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;
using AutoMapper;
using BL.DTO;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class UserService : IUserService
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMessageService MessageService;
        protected readonly IRepository<UserProfile> Profiles;
        protected readonly IRepository<UserIdentity> Identities;

        public UserService(IUnitOfWork unitOfWork, IMessageService messageService)
        {
            UnitOfWork = unitOfWork;
            MessageService = messageService;

            Profiles = UnitOfWork.UserProfiles;
            Identities = UnitOfWork.UserIdentities;
        }

        public async Task<OperationResult> CreateUser(UserDTO user)
        {
            var id = Guid.NewGuid();
            var identity = new UserIdentity { Id = id };

            var profile = Mapper.Map<UserProfile>(user);
            profile.Id = id;
            profile.Identity = identity;

            try
            {
                Profiles.Insert(profile);
                await UnitOfWork.SaveAsync();

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var result = new OperationResult(false);
                result.Errors.Add(ex.Message);
                return result;
            }
        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            var user = await Profiles.Get(id);

            return user != null ? Mapper.Map<UserDTO>(user) : null;
        }

        public async Task<UserDTO> GetUser(string loginOrEmail, string password)
        {
            var user = await Profiles.Get(t => 
                (t.Login == loginOrEmail || t.Email == loginOrEmail) && 
                t.Password == password);

            return user != null ? Mapper.Map<UserDTO>(user) : null;
        }

        public async Task<bool> CheckLoginOccuped(string login)
        {
            var user = await Profiles.Get(t => t.Login == login);

            return user != null;
        }

        public async Task<bool> CheckEmailOccuped(string email)
        {
            var user = await Profiles.Get(t => t.Email == email);

            return user != null;
        }

        public async Task<OperationResult> SendConfirmationMessage(string code)
        {
            try
            {
                await MessageService.SendMessageAsync(code, "", "");

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var result = new OperationResult(false);
                result.Errors.Add(ex.Message);

                return result;
            }
        }

        public async Task<Guid?> CreateVerificationToken(Guid id)
        {
            var identity = await Identities.Get(id);
            var token = Guid.NewGuid();

            if (identity == null) return null;

            if (identity.AccountVerificationToken == null)
            {
                identity.AccountVerificationToken = token;
                UnitOfWork.Update(identity);
                await UnitOfWork.SaveAsync();
            }

            return identity.AccountVerificationToken;
        }

        public async Task<OperationResult> ConfirmAccount(Guid token)
        {
            var identity = await Identities.Get(t => t.AccountVerificationToken == token);

            if (identity == null)
            {
                var result = new OperationResult(false);
                result.Errors.Add("Объект с данным ключом не найден");
                return result;
            }

            identity.AccountVerificationToken = null;
            identity.AccountVerified = true;
            UnitOfWork.Update(identity);
            await UnitOfWork.SaveAsync();

            return new OperationResult(true);
        }
    }
}
