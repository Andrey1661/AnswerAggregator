using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;
using BL.DTO;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class TopicService : ServiceBase, ITopicService
    {
        protected readonly IRepository<Topic> Topics;
        protected readonly IRepository<Post> Posts;
        protected readonly IRepository<Subject> Subjects; 

        public TopicService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            Topics = UnitOfWork.GetRepository<Topic>();
            Posts = UnitOfWork.GetRepository<Post>();
            Subjects = UnitOfWork.GetRepository<Subject>();
        }

        public async Task<IEnumerable<TopicDTO>> GetTopicList(Guid subjectId)
        {
            var topis = await Topics.GetList(t => t.SubjectId == subjectId);
            var dto = topis.Select(t => new TopicDTO
            {
                Id = t.Id,
                Title = t.Title
            });

            return dto;
        }

        public async Task<TopicDTO> GetTopic(Guid topicId, int page, int pageSize)
        {
            var topic = await Topics.Get(topicId);

            var dto = new TopicDTO
            {
                Id = topic.Id,
                Title = topic.Title,
                Posts = topic.Posts.Skip((page - 1)*pageSize).Take(pageSize).Select(post => new PostDTO
                {
                    Id = post.Id,
                    Author = post.Author.Email,
                    Content = post.Content,
                    CreationDate = post.CreationDate
                }).ToList()
            };

            return dto;
        }

        public async Task<IEnumerable<PostDTO>> GetPosts(Guid topicId, int page, int pageSize)
        {
            var posts = await Posts.GetList(t => t.TopicId == topicId, t => t.CreationDate, (page + 1)*pageSize, pageSize);
            var dto = posts.Select(t => new PostDTO
            {
                Id = t.Id,
                CreationDate = t.CreationDate,
                Content = t.Content,
                Author = t.Author.Email
            });

            return dto;
        }

        public async Task CreateTopic(string title, Guid subjectId, string auhtor)
        {
            var user = await UnitOfWork.GetRepository<UserProfile>().Get(t => t.Email == auhtor);

            var topic = new Topic
            {
                Id = Guid.NewGuid(),
                SubjectId = subjectId,
                CreationDate = DateTime.Now,
                Title = title,
                AuthorId = user.Id
            };

            Topics.Insert(topic);
            await UnitOfWork.SaveAsync();
        }

        public async Task AddPost(Guid topicId, PostDTO post)
        {
            var author = await UnitOfWork.GetRepository<UserProfile>().Get(t => t.Email == post.Author);

            var newPost = new Post
            {
                Content = post.Content,
                Id = Guid.NewGuid(),
                TopicId = topicId,
                AuthorId = author.Id,
                CreationDate = DateTime.Now
            };

            Posts.Insert(newPost);
            await UnitOfWork.SaveAsync();
        }

        public async Task AddAnswer(Guid parentPostId, PostDTO post)
        {
            var author = await UnitOfWork.GetRepository<UserProfile>().Get(t => t.Email == post.Author);

            var newPost = new Post
            {
                Content = post.Content,
                Id = Guid.NewGuid(),
                ParentPostId = parentPostId,
                AuthorId = author.Id,
                CreationDate = DateTime.Now
            };

            Posts.Insert(newPost);
            await UnitOfWork.SaveAsync();
        }
    }
}
