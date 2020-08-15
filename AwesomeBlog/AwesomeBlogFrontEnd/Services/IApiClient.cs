using AwesomeBlogDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogFrontEnd.Services
{
    public interface IApiClient
    {
        Task<BloggerResponse> GetBloggerAsync(string name);

        Task<List<Article>> GetArticlesAsync();
        Task<ArticleResponse> GetArticleAsync(int id);
        Task<bool> AddArticleAsync(Article article);
        Task DeleteArticle(int id);

        Task<List<Tag>> GetTagsAsync();
        Task<List<Tag>> GetTagsPopularAsync();
        Task<bool> AddTag(int articleId, int tagId);
        Task RemoveTag(int articleId, int tagId);

        Task<bool> AddComment(int articleId, Comment comment);
        Task RemoveComment(int articleId, int commentId);
    }
}
