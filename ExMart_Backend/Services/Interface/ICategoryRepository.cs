using ExMart_Backend.Model;
using System.Threading.Tasks;

namespace ExMart_Backend.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> RemoveCategoryAsync(int categoryId);
        Task<Category> UpdateCategoryAsync(Category category); // New method
    }
}
