using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    // ReSharper disable once InconsistentNaming
    public class PostDTO
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
