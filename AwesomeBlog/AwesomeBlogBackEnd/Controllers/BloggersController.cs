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

        [HttpGet]
        public async Task<ActionResult<List<AwesomeBlogDTO.Blogger>>> GetBloggers()
        {
            var bloggers = await _context.Bloggers.AsNoTracking()
                .Select(m => new AwesomeBlogDTO.Blogger
                {
                    Id = m.Id,
                    Name = m.Name,
                    LastName = m.LastName,
                    NickName = m.NickName,
                    Email = m.Email,
                    Bio = m.Bio
                })
                .ToListAsync();

            return bloggers;
        }

        // GET: api/bloggers/find/bob911
        [HttpGet("find/{name}")]
        public async Task<ActionResult<AwesomeBlogDTO.BloggerResponse>> GetBloggerByName(string name)
        {
            var blogger = await _context.Bloggers.AsNoTracking()
                .Include(a => a.Articles)
                .SingleOrDefaultAsync(n => n.NickName == name);

            if (blogger == null)
            {
                return NotFound();
            }

            return new AwesomeBlogDTO.BloggerResponse
            {
                Id = blogger.Id,
                Name = blogger.Name,
                LastName = blogger.LastName,
                NickName = blogger.NickName,
                Email = blogger.Email,
                Bio = blogger.Bio,
                Articles = blogger.Articles?
                .Select(a => new AwesomeBlogDTO.Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Body = a.Body,
                    Published = a.Published,
                    BloggerId = a.BloggerId
                }).ToList()
            };
        }

        // GET: api/bloggers/3
        [HttpGet("{id}")]
        public async Task<ActionResult<AwesomeBlogDTO.BloggerResponse>> GetBloggerById(int id)
        {
            var blogger = await _context.Bloggers.AsNoTracking()
                .Include(a => a.Articles)
                .SingleOrDefaultAsync(n => n.Id == id);

            if (blogger == null)
            {
                return NotFound();
            }

            return new AwesomeBlogDTO.BloggerResponse
            {
                Id = blogger.Id,
                Name = blogger.Name,
                LastName = blogger.LastName,
                NickName = blogger.NickName,
                Email = blogger.Email,
                Bio = blogger.Bio,
                Articles = blogger.Articles?
                .Select(a => new AwesomeBlogDTO.Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    Body = a.Body,
                    Published = a.Published,
                    BloggerId = a.BloggerId
                }).ToList()
            };
        }

        // PUT: api/bloggers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogger(int id, AwesomeBlogDTO.Blogger input)
        {
            if (!BloggerExists(id))
            {
                return NotFound();
            }

            var blogger = await _context.Bloggers.FindAsync(id);

            blogger.Id = input.Id;
            blogger.Name = input.Name;
            blogger.LastName = input.LastName;
            blogger.NickName = input.NickName;
            blogger.Email = input.Email;
            blogger.Bio = input.Bio;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/bloggers
        [HttpPost]
        public async Task<ActionResult<AwesomeBlogDTO.Blogger>> PostBlogger(AwesomeBlogDTO.Blogger input)
        {
            if (NickNameExists(input.NickName))
            {
                return BadRequest("The given 'nickname' is already taken by another user");
            }

            if (EmailExists(input.Email))
            {
                return BadRequest("The given 'email' is already in use by another account");
            }

            var blogger = new Blogger
            {
                Name = input.Name,
                LastName = input.LastName,
                NickName = input.NickName,
                Email = input.Email,
                Bio = input.Bio
            };

            _context.Bloggers.Add(blogger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogger", new { name = blogger.NickName }, blogger);
        }

        // DELETE: api/bloggers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AwesomeBlogDTO.Blogger>> DeleteBlogger(int id)
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

        private bool NickNameExists(string name)
        {
            return _context.Bloggers.Any(e => e.NickName == name);
        }

        private bool EmailExists(string email)
        {
            return _context.Bloggers.Any(e => e.Email == email);
        }
    }
}
