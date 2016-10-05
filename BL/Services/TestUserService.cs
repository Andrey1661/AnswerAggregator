using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories;
using AnswerAggregator.Domain.Repositories.Interfaces;
using BL.DTO;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class TestUserService : IUserService
    {
        protected readonly IUnitOfWork UnitOfWork;

        public TestUserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            UnitOfWork.UserProfiles.GetAll().Wait();
        }

        public Task<OperationResult> CreateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUser(string loginOrEmail, string password)
        {
            var result = await UnitOfWork.UserProfiles.Get(t => t.Login == loginOrEmail);

            return new UserDTO
            {
                Id = result.Id,
                Login = result.Login,
                Name = result.Name,
                Surname = result.Surname,
                Patronymic = result.Patronymic
            };
        }

        public Task<UserDTO> GetUser(System.Linq.Expressions.Expression<Func<UserDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task<Enviroment.OperationResult> IUserService.CreateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<Enviroment.OperationResult> SendConfirmationMessage(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateVerificationToken(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Enviroment.OperationResult> ConfirmAccount(Guid token)
        {
            throw new NotImplementedException();
        }
    }
}
