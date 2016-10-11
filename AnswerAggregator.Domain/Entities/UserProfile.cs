using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnswerAggregator.Domain.Entities
{
    [Table("Users")]
    public class UserProfile : BaseEntity
    {
        [Key, ForeignKey("Identity")]
        public override Guid Id { get; set; }

        [Required]
        [Index("LoginIndex", IsUnique = true)]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [Index("EmailIndex", IsUnique = true)]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Patronymic { get; set; }

        public virtual UserIdentity Identity { get; set; }

        [ForeignKey("Group")]
        public Guid? GroupId { get; set; }

        public virtual Group Group { get; set; }
    }

    class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            HasRequired(t => t.Identity)
                .WithRequiredDependent(t => t.Profile)
                .WillCascadeOnDelete(true);
        }
    }
}
