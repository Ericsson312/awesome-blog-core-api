using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Models
{
    public class Blogger : AwesomeBlogDTO.Blogger
    {
        public virtual ICollection<Article> Articles { get; set; }
    }
}