using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnswerAggregator.Domain.Entities
{
    public class Department : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Group> Groups { get; set; } 
    }
}
