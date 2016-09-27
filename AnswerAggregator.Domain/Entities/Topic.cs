using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class Topic : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }

        public virtual UserProfile Author { get; set; }

        public virtual ICollection<Post> Posts { get; set; } 
    }
}
