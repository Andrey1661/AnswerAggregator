using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnswerAggregator.Domain.Entities
{
    public class UserMessage : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        public bool IsRead { get; set; }

        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }

        [ForeignKey("Reciever")]
        public Guid RecieverId { get; set; }

        public virtual UserProfile Sender { get; set; }

        public virtual UserProfile Reciever { get; set; }
    }

    class UserMessageConfiguration : EntityTypeConfiguration<UserMessage>
    {
        public UserMessageConfiguration()
        {
            HasRequired(t => t.Sender)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Reciever)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
