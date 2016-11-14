using System;
using System.Collections.Generic;

namespace BL.DTO
{
    // ReSharper disable once InconsistentNaming
    public class TopicDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ICollection<PostDTO> Posts { get; set; } 
    }
}
