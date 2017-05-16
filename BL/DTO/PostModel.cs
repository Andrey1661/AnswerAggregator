using System;
using System.Collections.Generic;

namespace BL.DTO
{
    public class PostModel
    {
        public Guid? TopicId { get; set; }

        public Guid? ParentPostId { get; set; }

        public Guid AuthorId { get; set; }

        public string Content { get; set; }

        public IEnumerable<FileModel> AttachedFiles { get; set; } 
    }
}
