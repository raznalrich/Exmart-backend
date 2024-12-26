using ExMart_Backend.Data;
using ExMart_Backend.Interface;
using ExMart_Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Repository
{
    public class CategoryRepository:ICategoryRepository
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

    }
}
