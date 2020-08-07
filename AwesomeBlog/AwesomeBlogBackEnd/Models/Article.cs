using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Models
{
    public class Article : AwesomeBlogDTO.Article
    {
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
