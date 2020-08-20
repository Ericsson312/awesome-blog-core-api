using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBlogFrontEnd.Pages.Models;
using AwesomeBlogFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using AwesomeBlogFrontEnd.Middleware;

namespace AwesomeBlogFrontEnd
{
    [SkipWelcome]
    public class WelcomeModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public WelcomeModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public Blogger Blogger { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public IActionResult OnGetAsync()
        {
            // Redirect to home page if user is anonymous or already registered as blogger
            var isBlogger = User.IsBlogger();

            if (!User.Identity.IsAuthenticated || isBlogger)
            {
                return RedirectToPage("/Index");
            }

            Email = User.Identity.Name;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Blogger.Email = User.Identity.Name;

            var success = await _apiClient.AddBloggerAsync(Blogger);

            if (!success)
            {
                ModelState.AddModelError("", "There was an issue creating the blogger for this user.");
                return Page();
            }

            // Re-issue the auth cookie with the new IsAttendee claim
            User.MakeBlogger();
            await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, User);

            return RedirectToPage("/Index");
        }
    }
}