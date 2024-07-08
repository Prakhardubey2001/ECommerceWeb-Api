using ECommerce.CartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.CartAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<CartItems> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }

    }
}
