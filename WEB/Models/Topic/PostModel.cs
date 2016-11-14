using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models.Topic
{
    public class PostModel
    {
        [DataType(DataType.Html)]
        public string Content { get; set; }

        public DateTime CreationDate { get; set; }

        public string Author { get; set; }
    }
}