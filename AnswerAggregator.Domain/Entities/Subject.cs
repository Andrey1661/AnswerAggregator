using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnswerAggregator.Domain.Entities
{
    public class Subject : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<GroupSubject> GroupSubjects { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }  
    }
}
