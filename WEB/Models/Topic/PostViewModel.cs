
using System;
using System.Collections.Generic;
using BL.DTO;

namespace WEB.Models.Topic
{
    public class PostViewModel
    {
        public string Content { get; set; }

        public AuthorModel Author { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<ServerFileInfo> AttachedFiles { get; set; } 
    }
}