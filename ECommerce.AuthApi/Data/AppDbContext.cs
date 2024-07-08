using ECommerce.AuthApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.AuthApi.Data
{
    public class AppDbContext : IdentityDbContext<ECommerceUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var adminRoleId = "5ad93cd7-35e6-4fc7-9690-714f86ec8ef2";
            var userRoleId = "ac5e271a-005b-4ec8-8bdd-86571bdcdb1a";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };
            var users = new List<ECommerceUser>
            {
                new ECommerceUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin01@gmail.com",
                    Email = "admin01@gmail.com",
                    NormalizedEmail = "admin01@gmail.com",
                    EmailConfirmed = true,
                    Role = "Admin"
                },
                new ECommerceUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin02@gmail.com",
                    Email = "admin02@gmail.com",
                    NormalizedEmail = "admin02@gmail.com",
                    EmailConfirmed = true,
                    Role = "Admin"
                },
                new ECommerceUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user01@gmail.com",
                    Email = "user01@gmail.com",
                    NormalizedEmail = "user01@gmail.com",
                    EmailConfirmed = true,
                    Role = "User"
                },
                new ECommerceUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "user02@gmail.com",
                    Email = "user02@gmail.com",
                    NormalizedEmail = "user02@gmail.com",
                    EmailConfirmed = true,
                    Role = "User"
                }
            };
            var passwordHasher = new PasswordHasher<ECommerceUser>();
            foreach (var user in users)
            {

                user.PasswordHash = passwordHasher.HashPassword(user, "Demo@1234");
                user.SecurityStamp = user.Id;
            }
            builder.Entity<IdentityRole>().HasData(roles);
            builder.Entity<ECommerceUser>().HasData(users);
        }
    }
}


