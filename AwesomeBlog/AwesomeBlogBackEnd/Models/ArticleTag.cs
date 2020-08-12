using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Models
{
    public class ArticleTag
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
