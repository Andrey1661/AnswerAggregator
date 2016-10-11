using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

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

        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }

        public virtual UserProfile Author { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ICollection<Post> Posts { get; set; } 
    }

    class TopicConfiguration : EntityTypeConfiguration<Topic>
    {
        public TopicConfiguration()
        {
            HasRequired(t => t.Author)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
