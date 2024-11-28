
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearnStudentAPI
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }


        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                    new IdentityRole { ConcurrencyStamp = "1", Name = "Admin", NormalizedName = "Admin" },
                    new IdentityRole { ConcurrencyStamp = "2", Name = "User", NormalizedName = "User" },
                    new IdentityRole { ConcurrencyStamp = "3", Name = "AddStudentRole", NormalizedName = "AddStudentRole" }
                );
        }
    }
}
