using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AnswerAggregator.Domain.Entities
{
    public class Department : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Institute")]
        public Guid InstituteId { get; set; }

        public virtual Institute Institute { get; set; }

        public virtual ICollection<Group> Groups { get; set; } 
    }

    class DepratmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepratmentConfiguration()
        {
            HasRequired(t => t.Institute)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }
}
