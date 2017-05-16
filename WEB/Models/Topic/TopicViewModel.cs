using System;
using System.Collections.Generic;
using BL.DTO;

namespace WEB.Models.Topic
{
    public class TopicViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public AuthorModel Author { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<PostViewModel> Posts { get; set; } 
    }
}