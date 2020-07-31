using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Models
{
    public class Post : AwesomeBlogDTO.Post
    {
        public Blogger Blogger { get; set; }
    }
}
