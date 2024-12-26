using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
    }
}
