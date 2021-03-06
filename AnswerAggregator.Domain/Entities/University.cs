﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnswerAggregator.Domain.Entities
{
    public class University : BaseEntity
    {
        [MaxLength(250)]
        public string Name { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
    }
}
