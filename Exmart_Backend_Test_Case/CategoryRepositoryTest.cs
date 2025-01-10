using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Repository;
using ExMart_Backend.Services.Repository;
using Microsoft.EntityFrameworkCore;

namespace Exmart_Backend_Test_Case
{
    [TestFixture]
    public class CategoryRepositoryTest
    {
        private readonly ApplicationDBContext _db;
        private readonly CategoryRepository _categoryRepository;
        public CategoryRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _db = new ApplicationDBContext(options);
            _categoryRepository = new CategoryRepository(_db);

            SeedDate();
        }

        private void SeedDate()
        {
            _db.addToCategories.AddRange( new List<Category> {
                new Category { Id = 1, CategoryName = "Garments", IconPath = "icons/garments.png" },
                new Category { Id = 2, CategoryName = "Stationary", IconPath = "icons/stationary.png" },
                new Category { Id = 3, CategoryName = "Appliances", IconPath = "icons/appliance.png" }
            });
            _db.SaveChanges();
        }

        [Test]
        public async Task GetCategoriesAsync_ShouldReturnAllCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            Assert.AreEqual(4, categories.Count());
        }

        [Test]
        public async Task AddCategoryAsync_ShouldAddCategory()
        {
            var newCategory = new Category { CategoryName = "Electronics", IconPath = "icons/electronics.png" };
            var addedCategory = await _categoryRepository.AddCategoryAsync(newCategory);

            Assert.AreEqual(newCategory.CategoryName, addedCategory.CategoryName);
            Assert.AreEqual(4, (await _categoryRepository.GetCategoriesAsync()).Count());
        }

        [Test]
        public async Task RemoveCategoryAsync_ShouldRemoveCategory()
        {
            var categoryIdToRemove = 1;
            var removedCategory = await _categoryRepository.RemoveCategoryAsync(categoryIdToRemove);

            Assert.NotNull(removedCategory);
            Assert.AreEqual(categoryIdToRemove, removedCategory.Id);
            Assert.AreEqual(3, (await _categoryRepository.GetCategoriesAsync()).Count());   
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            _db.Database.EnsureDeleted();
            _db.Dispose();
        }

    }
}
