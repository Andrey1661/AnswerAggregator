using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicInfo>> GetTopicList(Guid subjectId);
        Task<TopicDto> GetTopic(Guid topicId, int page, int pageSize);
        Task<IEnumerable<PostDto>> GetPosts(Guid topicId, int page, int pageSize);
        Task CreateTopic(string title, Guid subjectId, Guid authorId);
        Task AddPost(PostModel post);
        Task EditPost(PostDto post);
    }
}
