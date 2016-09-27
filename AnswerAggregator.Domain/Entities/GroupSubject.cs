using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class GroupSubject
    {
        [Key, ForeignKey("Group")]
        public Guid GroupId { get; set; }

        [Key, ForeignKey("Subject")]
        public Guid SubjectId { get; set; }

        public virtual Group Group { get; set; }

        public virtual Subject Subject { get; set; }

        public int SemesterNumber { get; set; }
    }
}
