
using API.Entities;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BlogNewsContext : IdentityDbContext<AppUser>
    {
        public BlogNewsContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                new IdentityRole { Name = "Author", NormalizedName = "AUTHOR" },
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
            );
        }
    }
}