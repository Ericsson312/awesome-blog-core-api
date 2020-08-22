using AwesomeBlogDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AwesomeBlogFrontEnd.Services
{
    public class ApiClient : IApiClient
    {

        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Article> AddArticleAsync(Article article)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/articles/{article.BloggerId}", article);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Article>();
        }

        public async Task<bool> AddBloggerAsync(Blogger blogger)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/bloggers", blogger);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<bool> AddComment(int articleId, Comment comment)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/articles/{articleId}/comments", comment);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<bool> AddTag(int articleId, int tagId)
        {
            var response = await _httpClient.PostAsync($"/api/articles/{articleId}/tags/{tagId}", null);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task DeleteArticle(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/articles/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task<ArticleResponse> GetArticleAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/articles/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<ArticleResponse>();
        }

        public async Task<List<Article>> GetArticlesAsync()
        {
            var response = await _httpClient.GetAsync("/api/articles");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<Article>>();
        }

        public async Task<BloggerResponse> GetBloggerAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/bloggers/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<BloggerResponse>();
        }

        public async Task<BloggerResponse> GetBloggerByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var response = await _httpClient.GetAsync($"/api/bloggers/find/{name}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<BloggerResponse>();
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            var response = await _httpClient.GetAsync("/api/tags");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<Tag>>();
        }

        public async Task<List<Tag>> GetTagsPopularAsync()
        {
            var response = await _httpClient.GetAsync("/api/tags/popular");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<Tag>>();
        }

        public async Task PutArticleAsync(Article article)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/articles/{article.Id}", article);

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveAllTags(int articleId)
        {
            var response = await _httpClient.DeleteAsync($"/api/articles/{articleId}/tags/all");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveComment(int articleId, int commentId)
        {
            var response = await _httpClient.DeleteAsync($"/api/articles/{articleId}/comments/{commentId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveTag(int articleId, int tagId)
        {
            var response = await _httpClient.DeleteAsync($"/api/articles/{articleId}/tags/{tagId}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }
    }
}
