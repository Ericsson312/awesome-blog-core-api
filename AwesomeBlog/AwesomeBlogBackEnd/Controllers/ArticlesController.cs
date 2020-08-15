using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AwesomeBlogBackEnd.Models;
using AwesomeBlogBackEnd.Infrastructure;

namespace AwesomeBlogBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/articles
        [HttpGet]
        public async Task<ActionResult<List<AwesomeBlogDTO.Article>>> GetArticles()
        {
            var articles = await _context.Articles.AsNoTracking()
                .Select(a => new AwesomeBlogDTO.Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Body = a.Body,
                    BloggerId = a.BloggerId,
                    Published = a.Published
                })
                .ToListAsync();

            return articles;
        }

        // GET: api/articles/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AwesomeBlogDTO.ArticleResponse>> GetArticle(int id)
        {
            if (!ArticleExists(id))
            {
                return NotFound();
            }

            var article = await _context.Articles.AsNoTracking()
                .Include(a => a.Comments)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .SingleOrDefaultAsync(a => a.Id == id);

            var result = article.MapArticleResponse();

            return result;
        }

        // PUT: api/articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, AwesomeBlogDTO.Article input)
        {
            if (!ArticleExists(id))
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);

            article.Title = input.Title;
            article.Body = input.Body;
            article.Published = input.Published;
            article.BloggerId = input.BloggerId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/articles/2
        [HttpPost("{bloggerId}")]
        public async Task<ActionResult<AwesomeBlogDTO.Article>> PostArticle(int bloggerId, AwesomeBlogDTO.Article input)
        {

            var blogger = await _context.Bloggers.FindAsync(bloggerId);

            if (blogger == null)
            {
                return NotFound();
            }

            var article = new Article
            {
                Title = input.Title,
                Body = input.Body,
                Published = input.Published,
                BloggerId = bloggerId
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return article;
        }

        // POST: api/articles/2/tags/2
        [HttpPost("{articleId}/tags/{tagId}")]
        public async Task<ActionResult<AwesomeBlogDTO.ArticleResponse>> AddTag(int articleId, int tagId)
        {

            var article = await _context.Articles.Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .SingleOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(tagId);

            if (tag == null)
            {
                return BadRequest();
            }

            article.ArticleTags.Add(new ArticleTag
            {
                ArticleId = article.Id,
                TagId = tag.Id
            });
            await _context.SaveChangesAsync();

            var result = article.MapArticleResponse();

            return result;
        }

        // POST: api/articles/2/comments
        [HttpPost("{articleId}/comments")]
        public async Task<ActionResult<AwesomeBlogDTO.ArticleResponse>> AddComment(int articleId, AwesomeBlogDTO.Comment comment)
        {

            var article = await _context.Articles.Include(c => c.Comments).FirstOrDefaultAsync();

            if (article == null)
            {
                return NotFound();
            }

            article.Comments.Add(new Comment
            {
                Content = comment.Content,
                ArticleId = articleId,
                BloggerId = comment.BloggerId
            });

            await _context.SaveChangesAsync();

            var result = article.MapArticleResponse();

            return result;
        }

        // DELETE: api/articles/2/comments/2
        [HttpDelete("{articleId}/comments/{commentId}")]
        public async Task<ActionResult<AwesomeBlogDTO.ArticleResponse>> RemoveComment(int articleId, int commentId)
        {

            var article = await _context.Articles.FindAsync(articleId);

            if (article == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
            {
                return BadRequest();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            var result = article.MapArticleResponse();

            return result;
        }

        // DELETE: api/articles/2/tags/2
        [HttpDelete("{articleId}/tags/{tagId}")]
        public async Task<ActionResult<AwesomeBlogDTO.ArticleResponse>> RemoveTag(int articleId, int tagId)
        {

            var article = await _context.Articles.Include(a => a.ArticleTags)
                .SingleOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(tagId);

            if (tag == null)
            {
                return BadRequest();
            }

            var articleTag = article.ArticleTags.FirstOrDefault(at => at.TagId == tagId);
            article.ArticleTags.Remove(articleTag);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/articles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AwesomeBlogDTO.Article>> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            if (article.Comments != null)
            {
                _context.Comments.RemoveRange(article.Comments);
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
