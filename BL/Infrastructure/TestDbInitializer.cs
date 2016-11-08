using System;
using System.Data.Entity;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Entities;

namespace BL.Infrastructure
{
    class TestDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var id = Guid.NewGuid();

            var user = new UserProfile
            {
                Id = id,
                Login = "test",
                Password = "1111",
                Email = "test@mail.ru",
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                Identity = new UserIdentity
                {
                    Id = id,
                    AccountVerified = true,
                    Role = "Admin"
                }
            };

            context.UserProfiles.Add(user);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
