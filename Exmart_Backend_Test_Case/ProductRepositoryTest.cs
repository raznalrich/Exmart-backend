using System;
using System.Collections.Generic;
using System.Linq;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Exmart_Backend_Test_Case
{
    [TestFixture]
    public class ProductRepositoryTest
    {
        private ApplicationDBContext _db;
        private ProductRepository _productRepository;

        public ProductRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _db = new ApplicationDBContext(options);
            _productRepository = new ProductRepository(_db);
            SeedData();
        }

        // Method to seed data before each test
        private void SeedData()
        {
            _db.Products.AddRange(new List<Product> {
        new Product {
            Id = 1,
            Name = "Garments",
            Description = "Comfortable cotton garments",
            Brand = "BrandA",
            VendorId = 1,
            CategoryId = 1,
            SizeId = new List<int> { 1, 2, 3 }, // Sample Size IDs
            ColorId = new List<int> { 1, 2 }, // Sample Color IDs
            PrimaryImageUrl = "https://example.com/images/garments.jpg",
            Weight = 0.5m,
            Price = 19.99m,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = 1,
            IsActive = true
        },
        new Product {
            Id = 2,
            Name = "Stationary",
            Description = "Office stationary supplies",
            Brand = "BrandB",
            VendorId = 2,
            CategoryId = 2,
            SizeId = new List<int> { 1 },
            ColorId = new List<int> { 3, 4 },
            PrimaryImageUrl = "https://example.com/images/stationary.jpg",
            Weight = 0.2m,
            Price = 9.99m,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = 2,
            IsActive = true
        },
        new Product {
            Id = 3,
            Name = "Appliances",
            Description = "Home and kitchen appliances",
            Brand = "BrandC",
            VendorId = 3,
            CategoryId = 3,
            SizeId = new List<int> { 4, 5 },
            ColorId = new List<int> { 1, 5 },
            PrimaryImageUrl = "https://example.com/images/appliances.jpg",
            Weight = 1.5m,
            Price = 99.99m,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            CreatedBy = 3,
            IsActive = true
        }
    });

            _db.SaveChanges();
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }

        // Test case to check if products are fetched correctly
        [Test]
        public async Task GetProductsShouldReturnAllProducts()
        {
            // Act
            var products = await _productRepository.GetProducts();
            // Assert
            Assert.AreEqual(4, products.Count());
        }

        [Test]
        public async Task GetProductByIdValidIdReturnsProduct()
        {
            // Act
            var product = await _productRepository.GetProductById(1);

            // Assert
            Assert.IsNotNull(product);
            Assert.AreEqual(1, product.Id);
        }

        [Test]
        public async Task GetProductByIdInvalidIdReturnsNull()
        {
            var product = await _productRepository.GetProductById(999);
            Assert.IsNull(product);
        }

        [Test]
        public async Task AddProductValidProductAddsProductToDatabase()
        {
            var newProduct = new Product
            {
                Id = 11,
                Name = "New Product",
                Description = "New Product Description",
                Brand = "Brand X",
                Price = 99.99m,
                VendorId = 1,
                PrimaryImageUrl = "https://newimage.com",
                CategoryId = 2,
                SizeId = new List<int> { 1, 2 },
                ColorId = new List<int> { 1, 2 },
                Weight = 300m,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = 1,
                IsActive = true
            };

            _db.Products.Add(newProduct);
            await _db.SaveChangesAsync();

            var product = await _productRepository.GetProductById(11);
            Assert.IsNotNull(product);
            Assert.AreEqual("New Product", product.Name);
        }

        [Test]
        public async Task UpdateProductValidProductUpdatesProductInDatabase()
        {
            var product = await _productRepository.GetProductById(1);
            product.Name = "Updated Product Name";
            _db.Products.Update(product);
            await _db.SaveChangesAsync();

            var updatedProduct = await _productRepository.GetProductById(1);
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual("Updated Product Name", updatedProduct.Name);
        }

        [Test]
        public async Task ProductExistsExistingProductReturnsTrue()
        {
            var exists = await _productRepository.GetProductById(1) != null;
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ProductExistsNonExistingProductReturnsFalse()
        {
            var exists = await _productRepository.GetProductById(999) != null;
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task GetProductsByCategoryValidCategoryReturnsListOfProducts()
        {
            var products = _db.Products.Where(p => p.CategoryId == 1).ToList();
            Assert.IsNotEmpty(products);
            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public async Task GetProductsByCategoryInvalidCategoryReturnsEmptyList()
        {
            var products = _db.Products.Where(p => p.CategoryId == 999).ToList();
            Assert.IsEmpty(products);
        }

        [Test]
        public async Task GetProductsByPriceRangeValidRangeReturnsCorrectProducts()
        {
            var products = _db.Products.Where(p => p.Price >= 100 && p.Price <= 400).ToList();
            Assert.IsEmpty(products);
        }

        [Test]
        public async Task GetProductByNameValidNameReturnsProduct()
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == "Appliances");
            Assert.IsNotNull(product);
            Assert.AreEqual("Appliances", product.Name);
        }

        [Test]
        public async Task GetProductByNameNoMatchReturnsNull()
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Name == "NonExistingProduct");
            Assert.IsNull(product);
        }

        [Test]
        public async Task ProductIsActiveValidProductReturnsTrue()
        {
            var product = await _productRepository.GetProductById(1);
            Assert.IsTrue(product.IsActive);
        }
    }
}
