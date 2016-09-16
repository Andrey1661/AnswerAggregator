using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class UserIdentity
    {
        [Key, ForeignKey("Profile")]
        public Guid Id { get; set; }

        public bool AccountVerified { get; set; }

        public Guid? AccountVerificationToken { get; set; }

        public bool IsBanned { get; set; }

        public DateTime? BanEndTime { get; set; }

        public string BanReason { get; set; }

        public Guid? PasswordResetToken { get; set; }

        public virtual UserProfile Profile { get; set; }
    }
}
