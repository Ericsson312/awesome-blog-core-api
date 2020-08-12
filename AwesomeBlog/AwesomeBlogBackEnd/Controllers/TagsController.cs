using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBlogBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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