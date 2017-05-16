using System;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models.Topic
{
    public class CreateTopicModel
    {
        [Required(ErrorMessage = "Необходимо указать название")]
        public string Title { get; set; }

        public Guid SubjectId { get; set; }
    }
}