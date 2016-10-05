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
using BL.Services.Interfaces;

namespace BL.Services
{
    public class TestUserService : IUserService
    {
        protected readonly IUnitOfWork UnitOfWork;

        public TestUserService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            CreateTestUsers();
        }

        public Task CreateUser(UserDTO user)
        {
            throw new NotImplementedException();
        }


        public async Task<UserDTO> GetUser(string login)
        {
            var result = await UnitOfWork.UserProfiles.Get(t => t.Login == login);

            return new UserDTO
            {
                Id = result.Id,
                Login = result.Login,
                Name = result.Name,
                Surname = result.Surname,
                Patronymic = result.Patronymic
            };
        }

        private void CreateTestUsers()
        {
            var identity = new UserIdentity {Id = Guid.NewGuid()};

            var user = new UserProfile
            {
                Id = identity.Id,
                Login = "Test",
                Name = "Name",
                Surname = "Surname",
                Patronymic = "Patronymic",
                Identity = identity
            };

            UnitOfWork.UserProfiles.Insert(user);
            UnitOfWork.Save();
        }


        public Task<UserDTO> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUser(string loginOrEmail, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUser(System.Linq.Expressions.Expression<Func<UserDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
