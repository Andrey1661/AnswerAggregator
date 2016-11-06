using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnswerAggregator.Domain.Entities
{
    public class Group : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        public DateTime DateOfEntering { get; set; }

        [ForeignKey("Institute")]
        public Guid InstituteId { get; set; }

        public virtual Institute Institute { get; set; }

        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<UserProfile> Students { get; set; }

        public virtual ICollection<GroupSubject> GroupSubjects { get; set; }
    }

    class GroupConfiguration : EntityTypeConfiguration<Group>
    {
        public GroupConfiguration()
        {
            HasRequired(t => t.Institute)
                .WithMany()
                .WillCascadeOnDelete(false);

            HasRequired(t => t.Department)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
