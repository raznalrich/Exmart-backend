using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task<IEnumerable<object>> GetProducts();
        Task<Product> AddProductAsync(Product product);
    }
}
