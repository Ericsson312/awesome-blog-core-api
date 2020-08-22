using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBlogDTO;
using AwesomeBlogFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AwesomeBlogFrontEnd
{
    public class AddArticleModel : PageModel
    {

        private readonly IApiClient _apiClient;

        public AddArticleModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [BindProperty]
        public Article Article { get; set; } = new Article();

        [BindProperty]
        public List<int> SelectedTagsId { get; set; } = new List<int>();

        [BindProperty]
        public List<SelectListItem> TagsToSelect { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGet(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Article.BloggerId = id;

                var tags = await _apiClient.GetTagsAsync();

                TagsToSelect = tags.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();
            }
            else
            {
                RedirectToPage("./Account/Login");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Article.Published = DateTime.Now;

            var article = await _apiClient.AddArticleAsync(Article);

            for (int i = 0; i < SelectedTagsId.Count; i++)
            {
                await _apiClient.AddTag(article.Id, SelectedTagsId[i]);
            }

            return RedirectToPage("./Article", new { id = article.Id });
        }
    }
}