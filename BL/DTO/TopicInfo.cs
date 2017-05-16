using System;

namespace BL.DTO
{
    public class TopicInfo
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public AuthorModel Author { get; set; }
    }
}
