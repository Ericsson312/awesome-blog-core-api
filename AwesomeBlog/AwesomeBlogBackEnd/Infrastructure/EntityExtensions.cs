using AwesomeBlogBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Infrastructure
{
    public static class EntityExtensions
    {
        public static AwesomeBlogDTO.ArticleResponse MapArticleResponse(this Article article)
        {
            return new AwesomeBlogDTO.ArticleResponse
            {
                Id = article.Id,
                Title = article.Title,
                Body = article.Body,
                BloggerId = article.BloggerId,
                Published = article.Published,

                Comments = article.Comments?
                    .Select(c => new AwesomeBlogDTO.Comment
                    {
                        Id = c.Id,
                        Content = c.Content,
                        ArticleId = c.ArticleId,
                        Published = c.Published,
                        BloggerId = c.BloggerId
                    }).ToList(),

                Tags = article.ArticleTags?
                    .Select(t => new AwesomeBlogDTO.Tag
                    {
                        Id = t.Tag.Id,
                        Name = t.Tag.Name
                    }).ToList()
            };
        }
    }
}
