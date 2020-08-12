using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBlogBackEnd.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogger>()
                .HasIndex(n => n.NickName)
                .IsUnique();

            modelBuilder.Entity<Blogger>()
                .HasIndex(n => n.Email)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Many-to-many: Article <-> Tag
            modelBuilder.Entity<ArticleTag>()
                    .HasKey(at => new { at.ArticleId, at.TagId });
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Blogger> Bloggers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}
