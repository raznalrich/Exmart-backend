using ExMart_Backend.Data;
using ExMart_Backend.Interface;
using ExMart_Backend.Model;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                return await _context.addToCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error in GetCategoriesAsync: {ex.Message}");
                throw; 
            }
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            try
            {
                _context.addToCategories.Add(category);
                await _context.SaveChangesAsync();
                return category;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error in AddCategoryAsync: {ex.Message}");
                throw; 
            }
        }

        public async Task<Category> RemoveCategoryAsync(int categoryId)
        {
            try
            {
                var category = await _context.addToCategories.FindAsync(categoryId);
                if (category != null)
                {
                    _context.addToCategories.Remove(category);
                    await _context.SaveChangesAsync();
                }
                return category;
            }
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error in RemoveCategoryAsync: {ex.Message}");
                throw; 
            }
        }
    }
}
