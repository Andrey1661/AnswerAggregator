using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswerAggregator.Domain.Entities;
using AnswerAggregator.Domain.Repositories.Interfaces;
using AutoMapper;
using BL.DTO;
using BL.Enviroment;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class TopicService : ServiceBase, ITopicService
    {
        protected const string PostFilesFolderName = "Posts";

        protected readonly IRepository<Topic> Topics;
        protected readonly IRepository<Post> Posts;
        protected readonly IRepository<Subject> Subjects;

        protected readonly IFileManager FileManager;

        public TopicService(IUnitOfWork unitOfWork, IFileManager fileManager)
            : base(unitOfWork)
        {
            Topics = UnitOfWork.GetRepository<Topic>();
            Posts = UnitOfWork.GetRepository<Post>();
            Subjects = UnitOfWork.GetRepository<Subject>();

            FileManager = fileManager;
        }

        public async Task<IEnumerable<TopicInfo>> GetTopicList(Guid subjectId)
        {
            var topic = (await Topics.Include(t => t.Author.Identity).GetList(t => t.SubjectId == subjectId)).ToList();

            var dto = topic.Select(t => new TopicInfo
            {
                Id = t.Id,
                Title = t.Title,
                Author = new AuthorModel(t.Author.Email, 
                    FileManager.GetAvatar(t.Author.Email), 
                    t.Author.Identity.Role, 
                    string.Concat(t.Author.Surname, " ", t.Author.Name, " ", t.Author.Patronymic)),
                CreationDate = t.CreationDate
            });

            return dto;
        }

        public async Task<TopicDto> GetTopic(Guid topicId, int page, int pageSize)
        {
            var topic =
                await
                    Topics.Include(t => t.Author)
                        .Include(t => t.Author.Identity)
                        .Include(t => t.Posts.Select(p => p.Author.Identity))
                        .Get(topicId);

            var topicAuthor = Mapper.Map<UserProfile, AuthorModel>(topic.Author);
            topicAuthor.Avatar = FileManager.GetAvatar(topic.Author.Email);

            var dto = new TopicDto
            {
                Id = topic.Id,
                Title = topic.Title,
                Posts = topic.Posts.Skip((page - 1)*pageSize).Take(pageSize).Select(post => new PostDto
                {
                    Id = post.Id,
                    Author = new AuthorModel(post.Author.Email,
                        post.Author.Identity.Role,
                        FileManager.GetAvatar(post.Author.Email),
                        string.Concat(post.Author.Surname, " ", post.Author.Name, " ", post.Author.Patronymic)),
                    Content = post.Content,
                    CreationDate = post.CreationDate,
                    AttachedFiles = FileManager.GetPostFiles(post.Id)
                }),
                Author = topicAuthor
            };

            return dto;
        }

        public async Task<IEnumerable<PostDto>> GetPosts(Guid topicId, int page, int pageSize)
        {
            var posts =
                await
                    Posts
                        .Include(t => t.Author)
                        .Include(t => t.Topic)
                        .GetList(t => t.TopicId == topicId, t => t.CreationDate, (page + 1)*pageSize, pageSize);

            var dto = posts.Select(post => new PostDto
            {
                Id = post.Id,
                CreationDate = post.CreationDate,
                Content = post.Content,
                Author = new AuthorModel(post.Author.Email,
                        post.Author.Identity.Role,
                        FileManager.GetAvatar(post.Author.Email),
                        string.Concat(post.Author.Surname, " ", post.Author.Name, " ", post.Author.Patronymic)),
                AttachedFiles = FileManager.GetPostFiles(post.Id)
            });

            return dto;
        }

        public async Task CreateTopic(string title, Guid subjectId, Guid authorId)
        {
            var topic = new Topic
            {
                Id = Guid.NewGuid(),
                SubjectId = subjectId,
                CreationDate = DateTime.Now,
                Title = title,
                AuthorId = authorId
            };

            Topics.Insert(topic);
            await UnitOfWork.SaveAsync();
        }

        public async Task AddPost(PostModel post)
        {
            var newPost = new Post
            {
                Content = post.Content,
                Id = Guid.NewGuid(),
                AuthorId = post.AuthorId,
                CreationDate = DateTime.Now
            };

            if (post.TopicId.HasValue)
            {
                newPost.TopicId = post.TopicId.Value;
            }
            else if (post.ParentPostId.HasValue)
            {
                newPost.ParentPostId = post.TopicId;
            }
            else
            {
                throw new ValidationException("У поста должен быть задан идентификатор темы или родительского поста");
            }

            if (post.AttachedFiles != null && post.AttachedFiles.Any())
            {
                await FileManager.AddPostFilesAsync(newPost.Id, post.AttachedFiles);
            }

            Posts.Insert(newPost);
            await UnitOfWork.SaveAsync();
        }

        public async Task AddPost(PostModel post, Guid parentPostId)
        {
            var newPost = new Post
            {
                Content = post.Content,
                Id = Guid.NewGuid(),
                ParentPostId = parentPostId,
                AuthorId = post.AuthorId,
                CreationDate = DateTime.Now
            };

            Posts.Insert(newPost);
            await UnitOfWork.SaveAsync();
        }

        public async Task EditPost(PostDto post)
        {
            throw new NotImplementedException();
        }
    }
}
