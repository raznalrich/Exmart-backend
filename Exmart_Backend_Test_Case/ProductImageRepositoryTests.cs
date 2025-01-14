using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace Exmart_Backend_Test_Case
{
    [TestFixture]
    public class ProductImageRepositoryTests
    {
        private readonly ApplicationDBContext _dbC;
        private readonly ProductImageRepository _productImageRepository;

        public ProductImageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _dbC = new ApplicationDBContext(options);
            _productImageRepository = new ProductImageRepository(_dbC);
            SeedDatabase();
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _dbC.Database.EnsureDeleted();
            _dbC.Dispose();
        }
        private void SeedDatabase()
        {
            _dbC.Images.AddRange(
                new ProductImages { ImageId = 1, ImageUrl = "http://example.com/image1.jpg", ProductId = 1 },
                new ProductImages { ImageId = 2, ImageUrl = "http://example.com/image2.jpg", ProductId = 1 },
                new ProductImages { ImageId = 3, ImageUrl = "http://example.com/image3.jpg", ProductId = 2 }
            );
            _dbC.SaveChanges();
        }

        [Test]
        public async Task GetImagesByProductIdValidProductIdReturnsImages()
        {
            // Act
            var result = await _productImageRepository.GetImagesByProductId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(img => img.ImageId == 1));
            Assert.IsTrue(result.Any(img => img.ImageId == 2));
        }
        [Test]
        public async Task GetImagesByProductId_InvalidProductId_ReturnsEmptyList()
        {
            // Act
            var result = await _productImageRepository.GetImagesByProductId(99);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public async Task GetImagesByProductId_ValidProductIdWithNoImages_ReturnsEmptyList()
        {
            // Act
            var result = await _productImageRepository.GetImagesByProductId(3);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }
        [Test]
        public async Task GetImagesByProductId_DatabaseHasMultipleProducts_ReturnsCorrectImages()
        {
            // Act
            var result = await _productImageRepository.GetImagesByProductId(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("http://example.com/image3.jpg", result.First().ImageUrl);
        }
    }
}
