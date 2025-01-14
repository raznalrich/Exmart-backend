using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace Exmart_Backend_Test_Case
{
    [TestFixture]
    public class FeedbackRepositoryTest
    {
        private readonly ApplicationDBContext _db;
        private readonly FeedbackRepository _feedbackRepository;

        public FeedbackRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _db = new ApplicationDBContext(options);
            _feedbackRepository = new FeedbackRepository(_db);
            SeedData();
        }

        private void SeedData()
        {
            // First clear any existing data
            _db.Users.RemoveRange(_db.Users);
            _db.Feedbacks.RemoveRange(_db.Feedbacks);
            _db.SaveChanges();

            // Add test users
            _db.Users.AddRange(new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Test User 1",
                    Email = "test1@example.com",
                    Phone = "1234567890",
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    Name = "Test User 2",
                    Email = "test2@example.com",
                    Phone = "0987654321",
                    CreatedAt = DateTime.UtcNow
                }
            });

            // Add test feedbacks
            _db.Feedbacks.AddRange(new List<Feedback>
            {
                new Feedback
                {
                    FeedBackId = 1,
                    UserId = 1,
                    ProductName = "Test Product 1",
                    FeedBack = "Great product!"
                },
                new Feedback
                {
                    FeedBackId = 2,
                    UserId = 1,
                    ProductName = "Test Product 2",
                    FeedBack = "Could be better"
                },
                new Feedback
                {
                    FeedBackId = 3,
                    UserId = 2,
                    ProductName = "Test Product 3",
                    FeedBack = "Excellent quality"
                }
            });

            _db.SaveChanges();
        }

        [Test]
        public async Task GetFeedbacksByUserIdAsync_ShouldReturnAllFeedbacks()
        {
            // Act
            var feedbacks = await _feedbackRepository.GetFeedbacksByUserIdAsync();

            // Assert
            Assert.NotNull(feedbacks);
            Assert.AreEqual(4, feedbacks.Count());
        }

        [Test]
        public async Task AddFeedbackAsync_ShouldAddNewFeedback()
        {
            // Arrange
            var initialCount = (await _feedbackRepository.GetAllFeedbacksAsync()).Count();

            var newFeedback = new Feedback
            {
                UserId = 1,
                ProductName = "New Test Product",
                FeedBack = "New feedback content"
            };

            // Act
            var addedFeedback = await _feedbackRepository.AddFeedbackAsync(newFeedback);

            // Assert
            Assert.NotNull(addedFeedback);
            var finalCount = (await _feedbackRepository.GetAllFeedbacksAsync()).Count();
            Assert.AreEqual(initialCount + 1, finalCount);
            Assert.AreEqual(newFeedback.ProductName, addedFeedback.ProductName);
            Assert.AreEqual(newFeedback.FeedBack, addedFeedback.FeedBack);
        }

        [Test]
        public async Task GetAllFeedbacksAsync_ShouldReturnAllFeedbacks()
        {
            // Act
            var feedbacks = await _feedbackRepository.GetAllFeedbacksAsync();

            // Assert
            Assert.NotNull(feedbacks);
            Assert.AreEqual(4, feedbacks.Count());
            Assert.That(feedbacks.Any(f => f.ProductName == "Test Product 1"));
            Assert.That(feedbacks.Any(f => f.ProductName == "Test Product 2"));
            Assert.That(feedbacks.Any(f => f.ProductName == "Test Product 3"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }
    }
}
