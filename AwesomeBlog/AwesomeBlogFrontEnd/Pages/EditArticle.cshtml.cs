using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBlogDTO;
using AwesomeBlogFrontEnd.Services;
using AwesomeBlogFrontEnd.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace AwesomeBlogFrontEnd
{
    public class EditArticleModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public EditArticleModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        [BindProperty]
        public Article Article { get; set; }

        [BindProperty]
        public List<int> SelectedTagsId { get; set; } = new List<int>();

        [BindProperty]
        public List<SelectListItem> TagsToSelect { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var blogger = await _apiClient.GetBloggerByNameAsync(User.Identity.Name);

                var isOwner = blogger.Articles.Where(a => a.Id == id).Any();

                if (!isOwner && !User.IsAdmin())
                {
                    return RedirectToPage("./Index");
                }

                var article = await _apiClient.GetArticleAsync(id);

                Article = new Article
                {
                    Id = article.Id,
                    BloggerId = article.BloggerId,
                    Title = article.Title,
                    Body = article.Body,
                    Published = article.Published
                };

                var tags = await _apiClient.GetTagsAsync();

                TagsToSelect = tags.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

                foreach (var tag in article.Tags)
                {
                    SelectedTagsId.Add(tag.Id);
                }
            }
            else
            {
                RedirectToPage("./Login");
            }

            //var article = await _apiClient.GetArticleAsync(id);

            //Article = new Article
            //{
            //    Id = article.Id,
            //    Title = article.Title,
            //    Body = article.Body,
            //    Published = article.Published,
            //    BloggerId = article.BloggerId
            //};

            //var tags = await _apiClient.GetTagsAsync();

            //TagsToSelect = tags.Select(t => new SelectListItem
            //{
            //    Value = t.Id.ToString(),
            //    Text = t.Name
            //}).ToList();

            //foreach (var tag in article.Tags)
            //{
            //    SelectedTagsId.Add(tag.Id);
            //}

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Session updated successfully!";

            await _apiClient.PutArticleAsync(Article);

            var articles = (await _apiClient.GetArticleAsync(Article.Id)).Tags.Select(i => i.Id).ToArray();

            if (SelectedTagsId.Count > 0)
            {
                if (!articles.ItemsEqual(SelectedTagsId.ToArray()))
                {
                    await _apiClient.RemoveAllTags(Article.Id);

                    for (int i = 0; i < SelectedTagsId.Count; i++)
                    {
                        await _apiClient.AddTag(Article.Id, SelectedTagsId[i]);
                    }
                }
            }
            else
            {
                await _apiClient.RemoveAllTags(Article.Id);
            }

            return await this.OnGetAsync(Article.Id);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var session = await _apiClient.GetArticleAsync(id);

            if (session != null)
            {
                await _apiClient.DeleteArticle(id);
            }

            Message = "Session deleted successfully!";

            return RedirectToPage("/Index");
        }
    }
}