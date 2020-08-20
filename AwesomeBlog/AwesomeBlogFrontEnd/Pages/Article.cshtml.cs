using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AwesomeBlogDTO;
using AwesomeBlogFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AwesomeBlogFrontEnd
{
    public class ArticleModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public ArticleModel( IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public ArticleResponse Article { get; set; }

        [BindProperty]
        public Blogger Blogger { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            Article = await _apiClient.GetArticleAsync(id);

            if (Article == null)
            {
                return RedirectToPage("/Index");
            }

            Blogger = await _apiClient.GetBloggerAsync(Article.BloggerId);

            return Page();
        }
    }
}