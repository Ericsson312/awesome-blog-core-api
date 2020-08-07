using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AwesomeBlogBackEnd.Models;

namespace AwesomeBlogBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BloggersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Bloggers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogger>>> GetBloggers()
        {
            return await _context.Bloggers.ToListAsync();
        }

        // GET: api/Bloggers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blogger>> GetBlogger(int id)
        {
            var blogger = await _context.Bloggers.FindAsync(id);

            if (blogger == null)
            {
                return NotFound();
            }

            return blogger;
        }

        // PUT: api/Bloggers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogger(int id, Blogger blogger)
        {
            if (id != blogger.Id)
            {
                return BadRequest();
            }

            _context.Entry(blogger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BloggerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bloggers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Blogger>> PostBlogger(Blogger blogger)
        {
            _context.Bloggers.Add(blogger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogger", new { id = blogger.Id }, blogger);
        }

        // DELETE: api/Bloggers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Blogger>> DeleteBlogger(int id)
        {
            var blogger = await _context.Bloggers.FindAsync(id);
            if (blogger == null)
            {
                return NotFound();
            }

            _context.Bloggers.Remove(blogger);
            await _context.SaveChangesAsync();

            return blogger;
        }

        private bool BloggerExists(int id)
        {
            return _context.Bloggers.Any(e => e.Id == id);
        }
    }
}
