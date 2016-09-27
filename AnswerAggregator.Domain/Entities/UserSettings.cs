using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        [Key, ForeignKey("Profile")]
        public override Guid Id { get; set; }

        public UserProfile Profile { get; set; }
    }
}
