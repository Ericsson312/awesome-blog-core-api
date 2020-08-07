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
                .HasIndex(name => name.Name)
                .IsUnique();
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Blogger> Bloggers { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
