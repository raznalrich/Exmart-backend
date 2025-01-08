using ExMart_Backend.Data;
using ExMart_Backend.Interface;
using ExMart_Backend.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ExMart_Backend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.addToCategories.ToListAsync();
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.addToCategories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> RemoveCategoryAsync(int categoryId)
        {
            var category = await _context.addToCategories.FindAsync(categoryId);
            if (category != null)
            {
                _context.addToCategories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _context.addToCategories.FindAsync(category.Id);
            if (existingCategory == null)
            {
                return null;
            }

            // Update the properties
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.IconPath = category.IconPath;
            // Add any other properties that need to be updated

            _context.addToCategories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}
