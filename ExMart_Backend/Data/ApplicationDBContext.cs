using System.Drawing;
using ExMart_Backend.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace ExMart_Backend.Data
{
    public class ApplicationDBContext : IdentityDbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> Images { get; set; }
        public DbSet<ProductStatus> Status { get; set; }
        public DbSet<Category> addToCategories { get; set; }

        public DbSet<ColourMaster> ColourMaster { get; set; }
        public DbSet<SizeMaster> SizeMaster { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<StatusMaster> StatusMaster { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }




        //public DbSet<ProductImage> ProductImages { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
            .HasMany(p => p.ProductImages)    // Changed from Images to ProductImages to match the property name
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SizeMaster>().HasData(
                new SizeMaster { SizeId = 1,Size="XS"},
                new SizeMaster { SizeId = 2,Size="S"},
                new SizeMaster { SizeId = 3,Size="M"},
                new SizeMaster { SizeId = 4,Size="L"},
                new SizeMaster { SizeId = 5,Size="XL"},
                new SizeMaster { SizeId = 6,Size="XXL"},
                new SizeMaster { SizeId = 7,Size="XXXL"}
                );
            modelBuilder.Entity<ColourMaster>().HasData(
                new ColourMaster { ColorId = 1, ColorName = "red", ColorCode = "#FF0000" },
    new ColourMaster { ColorId = 2, ColorName = "blue", ColorCode = "#0000FF" },
    new ColourMaster { ColorId = 3, ColorName = "white", ColorCode = "#FFFFFF" },
    new ColourMaster { ColorId = 4, ColorName = "black", ColorCode = "#000000" },
    new ColourMaster { ColorId = 5, ColorName = "green", ColorCode = "#008000" },
    new ColourMaster { ColorId = 6, ColorName = "violet", ColorCode = "#8F00FF" },
    new ColourMaster { ColorId = 7, ColorName = "yellow", ColorCode = "#FFFF00" }
                );

            modelBuilder.Entity<StatusMaster>().HasData(
       new StatusMaster { Product_StatusId = 1, StatusName = "Pending" },
       new StatusMaster { Product_StatusId = 2, StatusName = "Shipped" },
       new StatusMaster { Product_StatusId = 3, StatusName = "Delivered" }
   );

            // Order to OrderItems (Cascade Delete)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductImages>().HasData(
                new ProductImages
                {
                    ImageId = 1,
                    ImageUrl = " https://assets.ajio.com/medias/sys_master/root/20240202/XzVa/65bd163a8cdf1e0df5e313a0/-1117Wx1400H-442273276-black-MODEL7.jpg",
                    ProductId = 1,
                }, 
                new ProductImages
                {
                    ImageId = 11,
                    ImageUrl = "https://assets.ajio.com/medias/sys_master/root/20240202/T3LR/65bd18b616fd2c6e6ad4ea90/-473Wx593H-442273276-black-MODEL2.jpg",
                    ProductId = 1,
                },
                new ProductImages
                {
                    ImageId = 12,
                    ImageUrl = "https://assets.ajio.com/medias/sys_master/root/20240202/0iIg/65bd0e568cdf1e0df5e2e7f1/-473Wx593H-442273276-black-MODEL6.jpg",
                    ProductId = 1,
                },

                new ProductImages
                {
                    ImageId = 2,
                    ImageUrl = " https://m.media-amazon.com/images/I/51XQKBTbJ7L._SX569_.jpg",
                    ProductId = 2,
                },
                new ProductImages
                {
                    ImageId = 13,
                    ImageUrl = " https://m.media-amazon.com/images/I/51U6dpsRaFL._SX569_.jpg",
                    ProductId = 2,
                },
                new ProductImages
                {
                    ImageId = 14,
                    ImageUrl = " https://m.media-amazon.com/images/I/51Nd4BLQelL._SX569_.jpg",
                    ProductId = 2,
                },
                 new ProductImages
                 {
                     ImageId = 3,
                     ImageUrl = "https://m.media-amazon.com/images/I/915Qebmr9XL._SX679_.jpg",
                     ProductId = 3,
                 },
                 new ProductImages
                 {
                     ImageId = 15,
                     ImageUrl = "https://m.media-amazon.com/images/I/915Qebmr9XL._SX679_.jpg",
                     ProductId = 3,
                 },
                 new ProductImages
                 {
                     ImageId = 16,
                     ImageUrl = "https://m.media-amazon.com/images/I/91RWzNCIEhL._SX679_.jpg",
                     ProductId = 3,
                 },
                  new ProductImages
                  {
                      ImageId = 4,
                      ImageUrl = "https://m.media-amazon.com/images/I/61F5lcvJHLL._SX522_.jpg",
                      ProductId = 4,
                  }, 
                  new ProductImages
                  {
                      ImageId = 17,
                      ImageUrl = "https://m.media-amazon.com/images/I/61vBE3t4x1L._SX522_.jpg",
                      ProductId = 4,
                  }, 
                  new ProductImages
                  {
                      ImageId = 18,
                      ImageUrl = "https://m.media-amazon.com/images/I/71QZbcvD65L._SX522_.jpg",
                      ProductId = 4,
                  },
                   new ProductImages
                   {
                       ImageId = 5,
                       ImageUrl = "https://m.media-amazon.com/images/I/51caXIXHv0L._SX679_.jpg",
                       ProductId = 5,
                   }, 
                   new ProductImages
                   {
                       ImageId = 19,
                       ImageUrl = "https://m.media-amazon.com/images/I/614uQNgsIbL._SX679_.jpg",
                       ProductId = 5,
                   }, 
                   new ProductImages
                   {
                       ImageId = 20,
                       ImageUrl = "https://m.media-amazon.com/images/I/616+eQhVmFL._SX679_.jpg",
                       ProductId = 5,
                   },
                    new ProductImages
                    {
                        ImageId = 6,
                        ImageUrl = "https://m.media-amazon.com/images/I/51zrU3wXApL._SX679_.jpg",
                        ProductId = 6,
                    },
                    new ProductImages
                    {
                        ImageId = 21,
                        ImageUrl = "https://m.media-amazon.com/images/I/61h02o8qvAL._SX679_.jpg",
                        ProductId = 6,
                    },
                    new ProductImages
                    {
                        ImageId = 22,
                        ImageUrl = "https://m.media-amazon.com/images/I/71kg31YtFHL._SX679_.jpg",
                        ProductId = 6,
                    },
                     new ProductImages
                     {
                         ImageId = 7,
                         ImageUrl = "https://m.media-amazon.com/images/I/61ZkbRBEBvL._SY675_.jpg",
                         ProductId = 7,
                     }, 
                     new ProductImages
                     {
                         ImageId = 23,
                         ImageUrl = "https://m.media-amazon.com/images/I/81rLH99Wj2L._SY675_.jpg",
                         ProductId = 7,
                     }, 
                     new ProductImages
                     {
                         ImageId = 24,
                         ImageUrl = "https://m.media-amazon.com/images/I/61R2cfPmcSL._SY675_.jpg",
                         ProductId = 7,
                     },
                      new ProductImages
                      {
                          ImageId = 8,
                          ImageUrl = "https://m.media-amazon.com/images/I/61lEskbCaoL._SY450_.jpg",
                          ProductId = 8,
                      }, 
                      new ProductImages
                      {
                          ImageId = 25,
                          ImageUrl = "https://m.media-amazon.com/images/I/51wp23mi8qL._SY450_.jpg",
                          ProductId = 8,
                      }, 
                      new ProductImages
                      {
                          ImageId = 26,
                          ImageUrl = "https://m.media-amazon.com/images/I/71YCCEPBguL._SY450_.jpg",
                          ProductId = 8,
                      },
                       new ProductImages
                       {
                           ImageId = 9,
                           ImageUrl = "https://m.media-amazon.com/images/I/516HwL0zZhL._SY450_.jpg",
                           ProductId = 9,
                       },
                       new ProductImages
                       {
                           ImageId = 27,
                           ImageUrl = "https://m.media-amazon.com/images/I/6190STfffwL._SY450_.jpg",
                           ProductId = 9,
                       },
                       new ProductImages
                       {
                           ImageId = 28,
                           ImageUrl = "https://m.media-amazon.com/images/I/61Lo553SSIL._SY450_.jpg",
                           ProductId = 9,
                       },
                        new ProductImages
                        {
                            ImageId = 10,
                            ImageUrl = "https://example.com/gproxsuperlight_1.jpg",
                            ProductId = 10,
                        },
                        new ProductImages
                        {
                            ImageId = 29,
                            ImageUrl = "https://example.com/gproxsuperlight_1.jpg",
                            ProductId = 10,
                        },
                        new ProductImages
                        {
                            ImageId = 30,
                            ImageUrl = "https://example.com/gproxsuperlight_2.jpg",
                            ProductId = 10,
                        }
                );

            //modelBuilder.Entity<Product>().HasData(
            // Order to User (Restrict Delete)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order to ProductStatus (Restrict Delete)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ProductStatus)
                .WithMany(ps => ps.Orders)
                .HasForeignKey(o => o.Product_StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem to Product (Restrict Delete)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem to Size (Restrict Delete)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Size)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(oi => oi.SizeId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItem to Color (Restrict Delete)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Color)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.ColorId)
                .OnDelete(DeleteBehavior.Restrict);


           
        
           

            // Seeding the User table
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    Phone = "1234567890",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    UserId = 2,
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    Phone = "0987654321",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    UserId = 3,
                    Name = "Alice Brown",
                    Email = "alicebrown@example.com",
                    Phone = "1122334455",
                    CreatedAt =  DateTime.UtcNow // Specific UTC DateTime
                }
            );



            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 12,
                    Name = "Wireless Mouse",
                    Description = "Ergonomic wireless mouse with 2.4 GHz connectivity",
                    Brand = "VAFS",
                    Price = 25,
                    VendorId = 1,
                    CategoryId = 001,
                    Size = ["XS", "S", "M"],
                    Color = ["Blue", "Green"],
                    Weight = 250,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1
                },
                new Product
                {
                    Id = 1,
                    Name = "Experion Tshirt",
                    Description = "Experion branded t-shirt",
                    Brand = "Experion",
                    Price = 1499.00m,
                    VendorId = 1,
                    PrimaryImageUrl= "staticimages/pro_tshirt.png",
                    CategoryId = 1,
                    Size = new List<string> { "15.6 inches" },
                    Color = new List<string> { "red", "black" },
                    Weight = 500m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                },
                new Product
                {
                    Id = 2,
                    Name = "Hoody Experion brand",
                    Description = "Experion branded hoodie",
                    Brand = "Experion",
                    Price = 399.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://media.karousell.com/media/photos/products/2023/4/29/gildan_zipup_hoodie_1682750904_29598b39.jpg",
                    CategoryId = 1,
                    Size = new List<string> { "Standard" },
                    Color = new List<string> { "white" },
                    Weight = 600m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,

                },
                new Product
                {
                    Id = 3,
                    Name = "Jersey Experion branded",
                    Description = "Experion branded jersey",
                    Brand = "Experion",
                    Price = 399.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/51C2ieRiU9L.jpg",

                    CategoryId = 1,
                    Size = new List<string> { "40mm", "44mm" },
                    Color = new List<string> { "black", "red" },
                    Weight = 400m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true
                },
                new Product
                {
                    Id = 4,
                    Name = "Earpods Swiss military",
                    Description = "Swiss military branded earpods",
                    Brand = "Swiss Military",
                    Price = 349.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/71RFdy6y6LL._SL1500_.jpg",

                    CategoryId = 3,
                    Size = new List<string> { "Standard" },
                    Color = new List<string> { "white", "blue" },
                    Weight = 50m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                },
                new Product
                {
                    Id = 5,
                    Name = "Water Bottle",
                    Description = "Reusable water bottle",
                    Brand = "VAFS",
                    Price = 399.00m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/71zFvtVuP1L._SL1500_.jpg",

                    CategoryId = 2,
                    Size = new List<string> { "41mm", "45mm" },
                    Color = new List<string> { "blue", "white", "green" },
                    Weight = 300m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                },
                new Product
                {
                    Id = 6,
                    Name = "Flask",
                    Description = "Insulated flask",
                    Brand = "VAFS",
                    Price = 349.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/41W9B1Ri4hL.jpg",

                    CategoryId = 2,
                    Size = new List<string> { "Standard" },
                    Color = new List<string> { "Black", "red" },
                    Weight = 400m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                },
                new Product
                {
                    Id = 7,
                    Name = "Nike Air Force 1",
                    Description = "Nike Air Force 1 sneakers",
                    Brand = "Nike",
                    Price = 99.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/61t0gIsFpjL._SY675_.jpg",

                    CategoryId = 2,
                    Size = new List<string> { "5", "6", "7", "8", "9", "10", "11", "12" },
                    Color = new List<string> { "White", "Black", "Red" },
                    Weight = 800m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                },
                new Product
                {
                    Id = 8,
                    Name = "Samsung Galaxy Buds 2 Pro",
                    Description = "Samsung wireless earbuds",
                    Brand = "Samsung",
                    Price = 1999.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/61KVX-MbIUL._SL1500_.jpg",

                    CategoryId = 3,
                    Size = new List<string> { "Standard" },
                    Color = new List<string> { "black", "White" },
                    Weight = 50m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 23, 15, 22, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                },
                new Product
                {
                    Id = 9,
                    Name = "Diary",
                    Description = "Personal diary",
                    Brand = "VAFS",
                    Price = 149.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "https://m.media-amazon.com/images/I/61eYApdaTDL._SL1100_.jpg",

                    CategoryId = 2,
                    Size = new List<string> { "Standard" },
                    Color = new List<string> { "Black", "White" },
                    Weight = 200m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    CreatedBy = 1, IsActive = true,
                },
                new Product
                {
                    Id = 10,
                    Name = "BackPack",
                    Description = "Multi-purpose backpack",
                    Brand = "VAFS",
                    Price = 149.99m,
                    VendorId = 1,
                    PrimaryImageUrl = "staticimages/pro_bag.png",

                    CategoryId = 2,
                    Size = new List<string> { "Standard" },
                    Color = new List<string> { "Black", "White" },
                    Weight = 700m,
                    CreatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    UpdatedAt = new DateTime(2023, 11, 22, 13, 37, 0, DateTimeKind.Utc),
                    CreatedBy = 1,
                    IsActive = true,
                }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Garments",
                    IconPath = "icons/garments.png"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Stationary",
                    IconPath = "icons/stationary.png"

                },
                 new Category
                 {
                     Id = 3,
                     CategoryName = "Appliances",
                     IconPath = "icons/appliance.png"

                 }
            );

            modelBuilder.Entity<Feedback>().HasData(
                new Feedback
                { 
                    FeedBackId = 1,
                    UserId = 1,
                    ProductName = "bag",
                    FeedBack = "Very good quality "
                }

                );

                    modelBuilder.Entity<Feedback>()
            .HasOne(f => f.User) // Reference the navigation property in Feedback
            .WithMany(u => u.Feedbacks) // Reference the navigation property in User
            .HasForeignKey(f => f.UserId); // Specify the foreign key property


                }








    }
}
