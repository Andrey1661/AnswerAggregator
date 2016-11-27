using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Models.Topic
{
    public class CreatePostModel
    {
        public Guid TopicId { get; set; }

        public string Text { get; set; }

        public IEnumerable<HttpPostedFileBase> AttachedFiles { get; set; }
    }
}