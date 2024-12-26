using System.Drawing;
using ExMart_Backend.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext;

namespace ExMart_Backend.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with 2.4 GHz connectivity",
                    Brand = "VAFS",
                    Price = 25,
                    VendorId = 1,
                    CategoryId = "C001",
                    Size = ["XS", "S", "M"],
                    Color = ["Blue", "Green"],
                    Weight = 250,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = 1
                }
                );
        }
    }
}
