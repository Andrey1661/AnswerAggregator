using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    [Table("Users")]
    public class UserIdentity : BaseEntity
    {
        [Key, ForeignKey("Profile")]
        public override Guid Id { get; set; }

        public string Role { get; set; }

        public bool AccountVerified { get; set; }

        public Guid? AccountVerificationToken { get; set; }

        public Guid? PasswordResetToken { get; set; }

        public virtual UserProfile Profile { get; set; }

        public UserIdentity()
        {
            AccountVerified = false;
        }
    }
}
