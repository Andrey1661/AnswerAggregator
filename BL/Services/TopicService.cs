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

        public Task CreateTopic(string title, Guid subjectId)
        {
            throw new NotImplementedException();
        }

        public Task AddPost(Guid topicId, PostDTO post)
        {
            throw new NotImplementedException();
        }

        public Task AddAnswer(Guid parentPostId, PostDTO post)
        {
            throw new NotImplementedException();
        }
    }
}
