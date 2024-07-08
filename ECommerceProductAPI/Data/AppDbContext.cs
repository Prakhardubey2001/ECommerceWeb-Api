using ECommerceProductAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                ProductName = "T-shirt",
                ProductDescription = "A nice T-shirt",
                Price = 150,
                Specification = "Hand Stiched 100 percent cotton"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                ProductName = "Coat",
                ProductDescription = "A new coat",
                Price = 999,
                Specification = "Fine and Elegant Design"

            });
            
        }
    }
}
