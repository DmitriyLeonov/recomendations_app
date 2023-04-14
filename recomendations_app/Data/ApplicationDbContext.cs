using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.Models;

namespace Recomendations_app.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminRole = new IdentityRole("Administrator");
            adminRole.NormalizedName = adminRole.Name.ToUpper();

            var userRole = new IdentityRole("User");
            userRole.NormalizedName = userRole.Name.ToUpper();

            builder.Entity<IdentityRole>().HasData(adminRole, userRole);

            builder.Entity<ReviewModel>()
                .HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "english",
                    p => new { p.ReviewBody, p.Title })
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");
        }

        public DbSet<ReviewModel>? Reviews { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<GradeModel>? Grades { get; set; }
        public DbSet<TagModel>? Tags { get; set; }
        public DbSet<LikeModel>? Likes { get; set; }
        public DbSet<SubjectModel>? Subjects { get; set; }
    }
}