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
    public class UserService : ServiceBase, IUserService
    {
        protected readonly IMessageManager MessageManager;
        protected readonly IRepository<UserProfile> Profiles;
        protected readonly IRepository<UserIdentity> Identities;

        public UserService(IUnitOfWork unitOfWork, IMessageManager messageManager) 
            : base(unitOfWork)
        {
            MessageManager = messageManager;

            Profiles = UnitOfWork.GetRepository<UserProfile>();
            Identities = UnitOfWork.GetRepository<UserIdentity>();
        }

        public async Task<OperationResult> CreateUser(UserModel user)
        {
            var id = Guid.NewGuid();
            var identity = new UserIdentity {Id = id, Role = UserRoles.User.ToString()};

            try
            {
                var profile = new UserProfile
                {
                    Id = id,
                    Identity = identity
                };
                Mapper.Map(user, profile);
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

            try
            {
                UnitOfWork.Update(identity);
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


        public async Task<UserLoginData> GetUserLoginData(Guid id)
        {
            var user = await Profiles.Get(id);

            return user != null ? Mapper.Map<UserLoginData>(user) : null;
        }

        public async Task<UserLoginData> GetUserLoginData(string loginOrEmail)
        {
            var user =  await Profiles.Include(t => t.Identity)
                .Get(t => t.Login == loginOrEmail || t.Email == loginOrEmail);

            return user != null
                ? (UserLoginData) Mapper.Map(user, new UserLoginData(), typeof (UserProfile), typeof (UserLoginData))
                : null;
        }

        public async Task<UserLoginData> GetUserLoginData(string loginOrEmail, string password)
        {
            var user = await Profiles.Include(t => t.Identity).
                Get(t => (t.Login == loginOrEmail || t.Email == loginOrEmail) && t.Password == password);

            return user != null
                ? (UserLoginData)Mapper.Map(user, new UserLoginData(), typeof(UserProfile), typeof(UserLoginData))
                : null;
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


        public async Task<Guid?> CreateVerificationToken(Guid id)
        {
            var identity = await Identities.Get(id);
            return await CreateVerificationToken(identity);
        }

        public async Task<Guid?> CreateVerificationToken(string loginOrEmail)
        {
            var identity = await Identities.Get(t => t.Profile.Login == loginOrEmail || t.Profile.Email == loginOrEmail);
            return await CreateVerificationToken(identity);
        }

        public async Task<OperationResult> CheckUserPasswordResetToken(Guid token)
        {
            var identity = await Identities.Get(t => t.PasswordResetToken == token);

            if (identity != null)
            {
                return new OperationResult(true);
            }

            var result = new OperationResult(false);
            result.Errors.Add("Ключ сброса пароля недействителен");

            return result;
        }


        public async Task<Guid?> CreatePasswordResetToken(Guid id)
        {
            var identity = await Identities.Get(id);
            return await CreatePasswordResetToken(identity);
        }

        public async Task<Guid?> CreatePasswordResetToken(string loginOrEmail)
        {
            var identity = await Identities.Get(t => t.Profile.Login == loginOrEmail || t.Profile.Email == loginOrEmail);
            return await CreatePasswordResetToken(identity);
        }

        public async Task<OperationResult> ResetUserPassword(Guid token, string newPassword)
        {
            var identity = await Identities.Get(t => t.PasswordResetToken == token);

            identity.Profile.Password = newPassword;
            identity.PasswordResetToken = null;

            try
            {
                UnitOfWork.Update(identity);
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


        public async Task<OperationResult> SendPasswordResetMessage(string code, string returnUrl)
        {
            try
            {
                await MessageManager.SendPasswordResetMessage(code, returnUrl);

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var result = new OperationResult(false);
                result.Errors.Add(ex.Message);

                return result;
            }
        }

        public async Task<OperationResult> SendConfirmationMessage(string code, string returnUrl)
        {
            try
            {
                await MessageManager.SendConfirmationMessage(code, returnUrl);

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var result = new OperationResult(false);
                result.Errors.Add(ex.Message);

                return result;
            }
        }



        private async Task<Guid?> CreateVerificationToken(UserIdentity identity)
        {
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

        private async Task<Guid?> CreatePasswordResetToken(UserIdentity identity)
        {
            var token = Guid.NewGuid();

            if (identity == null)
                return null;

            if (identity.PasswordResetToken == null)
            {
                identity.PasswordResetToken = token;
                UnitOfWork.Update(identity);
                await UnitOfWork.SaveAsync();
            }

            return identity.PasswordResetToken;
        }
    }
}
