using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("Topic")]
        public Guid TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        [ForeignKey("ParentPost")]
        public Guid ParentPostId { get; set; }

        public virtual Post ParentPost { get; set; }

        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }

        public virtual UserProfile Author { get; set; }

        public virtual ICollection<Post> Answers { get; set; }
    }
}
