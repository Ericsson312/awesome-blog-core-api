using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeBlogDTO
{
    public class Comment
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime Published { get; set; }
    }
}
