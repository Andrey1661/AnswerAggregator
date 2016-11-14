using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models.Topic
{
    public class TopicModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ICollection<PostModel> Posts { get; set; } 
    }
}