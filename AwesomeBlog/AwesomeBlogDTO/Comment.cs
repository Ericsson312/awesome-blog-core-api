using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeBlogDTO
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public DateTime Published { get; set; }

        public int BloggerId { get; set; }

        public int ArticleId { get; set; }
    }
}
