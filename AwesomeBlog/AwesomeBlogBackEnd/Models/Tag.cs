using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Models
{
    public class Tag : AwesomeBlogDTO.Tag
    {
        public virtual ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
