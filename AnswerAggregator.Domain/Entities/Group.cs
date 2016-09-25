using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnswerAggregator.Domain.Entities
{
    public class Group : BaseEntity
    {
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<UserProfile> Students { get; set; } 
    }
}
