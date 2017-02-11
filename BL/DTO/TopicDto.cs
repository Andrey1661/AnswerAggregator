using System.Collections.Generic;

namespace BL.DTO
{
    public class TopicDto : TopicInfo
    {
        public IEnumerable<PostDto> Posts { get; set; }
    }
}
