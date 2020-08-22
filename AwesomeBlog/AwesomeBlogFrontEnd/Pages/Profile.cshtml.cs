using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBlogDTO;
using AwesomeBlogFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AwesomeBlogFrontEnd
{
    public class Profile : PageModel
    {
        private readonly IApiClient _apiClient;

        public Profile(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public List<Article> Articles { get; set; }

        [BindProperty]
        public Blogger Blogger { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("./Account/Login");
            }

            var blogger = await _apiClient.GetBloggerByNameAsync(User.Identity.Name);

            Articles = blogger.Articles.OrderByDescending(date => date.Published).ToList();

            Blogger = new Blogger
            {
                Id = blogger.Id,
                Name = blogger.Name,
                LastName = blogger.LastName,
                NickName = blogger.NickName,
                Email = blogger.Email,
                Bio = blogger.Bio
            };

            return Page();
        }
    }
}