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
        public DbSet<ColourMaster> ColourMaster { get; set; }
        public DbSet<SizeMaster> SizeMaster { get; set; }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        modelBuilder.Entity<Product>().HasData(
    //            new Product
    //            {
    //                Id = 12,
    //                Name = "Wireless Mouse",
    //                Description = "Ergonomic wireless mouse with 2.4 GHz connectivity",
    //                Brand = "VAFS",
    //                Price = 25,
    //                VendorId = 1,
    //                CategoryId = "C001",
    //                Size = ["XS", "S", "M"],
    //                Color = ["Blue", "Green"],
    //                Weight = 250,
    //                CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //                UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //                CreatedBy = 1
    //            },
    //            new Product
    //            {
    //                Id = 1,
    //                Name = "Experion Tshirt",
    //                Description = "Experion branded t-shirt",
    //                Brand = "Experion",
    //                Price = 1499.00m,
    //                VendorId = 1,
    //                CategoryId = "GAR001",
    //                Size = new List<string> { "15.6 inches" },
    //                Color = new List<string> { "red", "black" },
    //                Weight = 500m,
    //                CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //                UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //                CreatedBy = 1
    //            },

    //new Product
    //{
    //    Id = 2,
    //    Name = "Hoody Experion brand",
    //    Description = "Experion branded hoodie",
    //    Brand = "Experion",
    //    Price = 399.99m,
    //    VendorId = 1,
    //    CategoryId = "GAR001",
    //    Size = new List<string> { "Standard" },
    //    Color = new List<string> { "white" },
    //    Weight = 600m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 3,
    //    Name = "Jersey Experion branded",
    //    Description = "Experion branded jersey",
    //    Brand = "Experion",
    //    Price = 399.99m,
    //    VendorId = 1,
    //    CategoryId = "GAR001",
    //    Size = new List<string> { "40mm", "44mm" },
    //    Color = new List<string> { "black", "red" },
    //    Weight = 400m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 4,
    //    Name = "Earpods Swiss military",
    //    Description = "Swiss military branded earpods",
    //    Brand = "Swiss Military",
    //    Price = 349.99m,
    //    VendorId = 1,
    //    CategoryId = "APP001",
    //    Size = new List<string> { "Standard" },
    //    Color = new List<string> { "white", "blue" },
    //    Weight = 50m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 5,
    //    Name = "Water Bottle",
    //    Description = "Reusable water bottle",
    //    Brand = "VAFS",
    //    Price = 399.00m,
    //    VendorId = 1,
    //    CategoryId = "STA001",
    //    Size = new List<string> { "41mm", "45mm" },
    //    Color = new List<string> { "blue", "white", "green" },
    //    Weight = 300m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 6,
    //    Name = "Flask",
    //    Description = "Insulated flask",
    //    Brand = "VAFS",
    //    Price = 349.99m,
    //    VendorId = 1,
    //    CategoryId = "STA001",
    //    Size = new List<string> { "Standard" },
    //    Color = new List<string> { "Black", "red" },
    //    Weight = 400m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 8,
    //    Name = "Nike Air Force 1",
    //    Description = "Nike Air Force 1 sneakers",
    //    Brand = "Nike",
    //    Price = 99.99m,
    //    VendorId = 1,
    //    CategoryId = "STA001",
    //    Size = new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" },
    //    Color = new List<string> { "White", "Black", "Red" },
    //    Weight = 800m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 9,
    //    Name = "Samsung Galaxy Buds 2 Pro",
    //    Description = "Samsung wireless earbuds",
    //    Brand = "Samsung",
    //    Price = 1999.99m,
    //    VendorId = 1,
    //    CategoryId = "APP001",
    //    Size = new List<string> { "Standard" },
    //    Color = new List<string> { "black", "White" },
    //    Weight = 50m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 10,
    //    Name = "Diary",
    //    Description = "Personal diary",
    //    Brand = "VAFS",
    //    Price = 149.99m,
    //    VendorId = 1,
    //    CategoryId = "STA001",
    //    Size = new List<string> { "Standard" },
    //    Color = new List<string> { "Black", "White" },
    //    Weight = 200m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //},

    //new Product
    //{
    //    Id = 11,
    //    Name = "BackPack",
    //    Description = "Multi-purpose backpack",
    //    Brand = "VAFS",
    //    Price = 149.99m,
    //    VendorId = 1,
    //    CategoryId = "STA001",
    //    Size = new List<string> { "Standard" },
    //    Color = new List<string> { "Black", "White" },
    //    Weight = 700m,
    //    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    UpdatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
    //    CreatedBy = 1
    //}
    //            );
    //    }
    }
}
