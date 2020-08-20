using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogDTO
{
    public class Blogger
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(200)]
        public virtual string LastName { get; set; }

        [Required]
        [StringLength(150)]
        public virtual string NickName { get; set; }

        [Required]
        [StringLength(256)]
        public virtual string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public virtual string Bio { get; set; }
    }
}