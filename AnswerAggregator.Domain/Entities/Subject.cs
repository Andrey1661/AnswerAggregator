using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

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

    class SubjectConfiguration : EntityTypeConfiguration<Subject>
    {
        public SubjectConfiguration()
        {
            HasMany(t => t.Teachers)
                .WithMany(t => t.Subjects)
                .Map(t => t.MapLeftKey("SubjectId").MapRightKey("TeacherId").ToTable("SubjectTeacher"));
        }
    }
}
