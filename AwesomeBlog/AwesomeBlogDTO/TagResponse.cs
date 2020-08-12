using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeBlogDTO
{
    public class TagResponse : Tag
    {
        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }
}
