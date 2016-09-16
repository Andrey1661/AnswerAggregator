using System;
using System.ComponentModel.DataAnnotations;

namespace AnswerAggregator.Domain.Entities
{
    public class Department
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
    }
}
