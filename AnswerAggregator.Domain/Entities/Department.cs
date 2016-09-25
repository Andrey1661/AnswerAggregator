﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AnswerAggregator.Domain.Entities
{
    public class Department : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
