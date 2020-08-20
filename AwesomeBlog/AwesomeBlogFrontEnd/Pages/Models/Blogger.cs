using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogFrontEnd.Pages.Models
{
    public class Blogger : AwesomeBlogDTO.Blogger
    {
        [DisplayName("First Name")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [DisplayName("Last Name")]
        public override string LastName { get => base.LastName; set => base.LastName = value; }

        [DisplayName("Nickname")]
        public override string NickName { get => base.NickName; set => base.NickName = value; }

        [DisplayName("Bio")]
        public override string Bio { get => base.Bio; set => base.Bio = value; }

        [DisplayName("Email Address")]
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
