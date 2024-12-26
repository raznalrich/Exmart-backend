using ExMart_Backend.Model;

namespace ExMart_Backend.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> RemoveCategoryAsync(int categoryId);
    }
}
