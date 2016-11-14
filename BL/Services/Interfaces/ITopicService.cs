using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDTO>> GetTopicList(Guid subjectId);
        Task<TopicDTO> GetTopic(Guid topicId, int page, int pageSize);
        Task<IEnumerable<PostDTO>> GetPosts(Guid topicId, int page, int pageSize);
        Task CreateTopic(string title, Guid subjectId);
        Task AddPost(Guid topicId, PostDTO post);
        Task AddAnswer(Guid parentPostId, PostDTO post);
    }
}
