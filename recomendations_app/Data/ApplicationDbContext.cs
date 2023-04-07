using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}