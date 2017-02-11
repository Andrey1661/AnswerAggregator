using System;
using System.Collections.Generic;

namespace BL.DTO
{
    // ReSharper disable once InconsistentNaming
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public AuthorModel Author { get; set; }

        public DateTime CreationDate { get; set; }

        public IEnumerable<ServerFileInfo> AttachedFiles { get; set; }
    }
}
