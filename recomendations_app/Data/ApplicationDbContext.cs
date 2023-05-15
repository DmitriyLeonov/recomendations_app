using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Recomendations_app.Models;

namespace Recomendations_app.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public ApplicationDbContext() : base()
        { }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=database-2.cwwsbjhfixu7.eu-central-1.rds.amazonaws.com;Port=5432;Database=recomendations_db;TrustServerCertificate=True;Integrated Security=false;User ID=postgres;Password=FuLtaWdROU;");
            base.OnConfiguring(optionsBuilder);
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
                .HasMany(c => c.Tags);

            builder.Entity<ReviewModel>()
                .HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "english",
                    p => new { p.ReviewBody, p.Title})
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");

            builder.Entity<Comment>()
                .HasGeneratedTsVectorColumn(
                    p => p.SearchVector,
                    "english",
                    p => p.CommentBody)
                .HasIndex(p => p.SearchVector)
                .HasMethod("GIN");
        }

        public new DbSet<UserModel>? Users { get; set; }
        public DbSet<ReviewModel>? Reviews { get; set; } = null!;
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<TagModel>? Tags { get; set; } = null!;
        public DbSet<LikeModel>? Likes { get; set; }
        public DbSet<ImageModel>? Images { get; set; }
    }
}