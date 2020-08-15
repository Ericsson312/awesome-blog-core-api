using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Article> Articles { get; private set; }

        [BindProperty]
        public IEnumerable<Tag> Tags { get; private set; }

        public async Task OnGetAsync()
        {
            Tags = await _apiClient.GetTagsPopularAsync();
            Articles = (await _apiClient.GetArticlesAsync()).OrderByDescending(a => a.Published);
        }
    }
}
