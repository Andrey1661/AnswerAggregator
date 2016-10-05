using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class Group : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        public Department Department { get; set; }

        public virtual ICollection<UserProfile> Students { get; set; }

        public virtual ICollection<GroupSubject> GroupSubjects { get; set; }
    }
}
