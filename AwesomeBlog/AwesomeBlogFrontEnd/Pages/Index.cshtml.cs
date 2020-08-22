using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AwesomeBlogDTO;
using AwesomeBlogFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AwesomeBlogFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IApiClient _apiClient;

        public IndexModel(ILogger<IndexModel> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        [BindProperty]
        public List<Article> Articles { get; private set; }

        [BindProperty]
        public List<Tag> Tags { get; private set; }

        [BindProperty]
        public bool IsAdmin { get; set; }

        [BindProperty]
        public int UserId { get; set; }

        public async Task OnGetAsync(int tag)
        {
            IsAdmin = User.IsAdmin();

            if (User.IsBlogger())
            {
                var blogger = await _apiClient.GetBloggerByNameAsync(User.Identity.Name);
                UserId = blogger.Id;
            }

            Tags = await _apiClient.GetTagsPopularAsync();

            if (tag != 0)
            {
                Articles = (await _apiClient.GetArticlesByTagIdAsync(tag)).OrderByDescending(a => a.Published).ToList();
            }
            else
            {
                Articles = (await _apiClient.GetArticlesAsync()).OrderByDescending(a => a.Published).ToList();
            }

        }
    }
}
