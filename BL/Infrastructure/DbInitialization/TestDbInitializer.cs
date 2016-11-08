using System;
using System.Collections.Generic;
using System.Data.Entity;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Entities;

namespace BL.Infrastructure.DbInitialization
{
    class TestDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var user = new UserProfile
            {
                Id = Guid.NewGuid(),
                Login = "test",
                Password = "1111",
                Email = "test@mail.ru",
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                Identity = new UserIdentity
                {
                    AccountVerified = true,
                    Role = "Admin"
                }
            };

            var universities = new List<University>
            {
                new University
                {
                    Id = Guid.NewGuid(),
                    Name = "Сибирский Федеральный Университет"
                },
           
                new University
                {
                    Id = Guid.NewGuid(),
                    Name = "Московский Государственный Университет"
                },

                new University
                {
                    Id = Guid.NewGuid(),
                    Name = "Сибирский Государственный Аэрокосмический Университет"
                },

                new University
                {
                    Id = Guid.NewGuid(),
                    Name = "Красноярский Государственный Аграрный Университет"
                }  
            };

            var institutes = new List<Institute>
            {
                new Institute
                {
                    Id = Guid.NewGuid(),
                    University = universities[0],
                    Name = "Институт космических и информационных технологий"
                },

                new Institute
                {
                    Id = Guid.NewGuid(),
                    University = universities[0],
                    Name = "Институт управления бизнес-процессами и экономики"
                }
            };

            var departments = new List<Department>
            {
                new Department
                {
                    Id = Guid.NewGuid(),
                    Institute = institutes[0],
                    Name = "Программная инженерия"
                },

                new Department
                {
                    Id = Guid.NewGuid(),
                    Institute = institutes[1],
                    Name = "Экономика"
                }
            };

            var groups = new List<Group>
            {
                new Group
                {
                    Id = Guid.NewGuid(),
                    Institute = institutes[0],
                    Department = departments[0],
                    Name = "КИ14-17Б",
                    DateOfEntering = DateTime.Parse("01-09-2014")
                },

                new Group
                {
                    Id = Guid.NewGuid(),
                    Institute = institutes[0],
                    Department = departments[0],
                    Name = "КИ13-16Б",
                    DateOfEntering = DateTime.Parse("01-09-2013")
                },

                new Group
                {
                    Id = Guid.NewGuid(),
                    Institute = institutes[1],
                    Department = departments[1],
                    Name = "УБ15-12Б",
                    DateOfEntering = DateTime.Parse("01-09-2015")
                },

                new Group
                {
                    Id = Guid.NewGuid(),
                    Institute = institutes[1],
                    Department = departments[1],
                    Name = "УБ16-10Б",
                    DateOfEntering = DateTime.Parse("01-09-2016")
                }
            };

            context.UserProfiles.Add(user);
            context.Universities.AddRange(universities);
            context.Groups.AddRange(groups);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
