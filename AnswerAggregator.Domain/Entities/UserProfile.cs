using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        [Key, ForeignKey("Identity")]
        public override Guid Id { get; set; }

        [MaxLength(50)]
        public string Login { get; set; }

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
}
