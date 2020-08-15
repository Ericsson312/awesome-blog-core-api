using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBlogBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeBlogBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/tags
        [HttpGet]
        public async Task<ActionResult<List<AwesomeBlogDTO.Tag>>> GetTags()
        {
            var tags = await _context.Tags.AsNoTracking()
                .Select(t => new AwesomeBlogDTO.Tag
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return tags;
        }

        // GET: api/tags/popular
        [HttpGet("popular")]
        public async Task<ActionResult<List<AwesomeBlogDTO.Tag>>> GetPopularTags()
        {
            var tags = await _context.Tags.OrderByDescending(t => t.ArticleTags.Count)
                .Take(5)
                .Select(t => new AwesomeBlogDTO.Tag
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return tags;
        }

        // POST: api/tags
        [HttpPost]
        public async Task<ActionResult> UploadTags([FromBody] IEnumerable<AwesomeBlogDTO.Tag> data)
        {
            List<Tag> tags = new List<Tag>();

            foreach (var tag in data)
            {
                tags.Add(new Tag
                {
                    Name = tag.Name
                });
            }

            _context.Tags.AddRange(tags);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}