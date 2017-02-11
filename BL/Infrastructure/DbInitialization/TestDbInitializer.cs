using System;
using System.Collections.Generic;
using System.Data.Entity;
using AnswerAggregator.Domain.Contexts;
using AnswerAggregator.Domain.Entities;

namespace BL.Infrastructure.DbInitialization
{
    class TestDbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var id = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA");
            var id2 = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB");

            var user = new UserProfile
            {
                Id = id,
                Login = "andrey",
                Password = "1111",
                Email = "shedogubov.andrey96@gmail.com",
                Name = "Андрей",
                Surname = "Шедогубов",
                Patronymic = "Александрович",
                Identity = new UserIdentity
                {
                    Id = id,
                    AccountVerified = true,
                    Role = "Admin"
                }
            };

            var user2 = new UserProfile
            {
                Id = id2,
                Login = "danil",
                Password = "1111",
                Email = "danil@mail.ru",
                Name = "Данил",
                Surname = "Вельтер",
                Patronymic = "Вячеславович",
                Identity = new UserIdentity
                {
                    Id = id2,
                    AccountVerified = true,
                    Role = "User"
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

            var groupSubjects = new List<GroupSubject>
            {
                new GroupSubject
                {
                    Group = groups[0],
                    Subject = new Subject
                    {
                        Id = Guid.NewGuid(),
                        Name = "Программный курсовой проект"
                    },
                    SemesterNumber = 5
                },

                new GroupSubject
                {
                    Group = groups[0],
                    Subject = new Subject
                    {
                        Id = Guid.NewGuid(),
                        Name = "Теория стохастических объектов"
                    },
                    SemesterNumber = 5
                },

                new GroupSubject
                {
                    Group = groups[1],
                    Subject = new Subject
                    {
                        Id = Guid.NewGuid(),
                        Name = "Мат. анализ"
                    },
                    SemesterNumber = 5
                }
            };

            user.Group = groups[0];
            user2.Group = groups[1];

            context.UserProfiles.Add(user);
            context.UserProfiles.Add(user2);
            context.GroupSubjects.AddRange(groupSubjects);
            context.Universities.AddRange(universities);
            context.Groups.AddRange(groups);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
