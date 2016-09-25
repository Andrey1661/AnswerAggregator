using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnswerAggregator.Domain.Entities
{
    public class Institute : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("University")]
        public Guid UniversityId { get; set; }

        public virtual University University { get; set; }
    }
}
