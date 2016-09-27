using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnswerAggregator.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(50)]
        public string Patronymic { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
