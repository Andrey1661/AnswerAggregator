using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Email = "test@mail.ru",
                Identity = new UserIdentity
                {
                    Id = id
                }
            };

            context.UserProfiles.Add(user);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
