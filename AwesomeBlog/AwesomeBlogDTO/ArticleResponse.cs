using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeBlogDTO
{
    public class ArticleResponse : Article
    {
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
