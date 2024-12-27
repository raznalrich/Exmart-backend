using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IProductImageRepository
    {
        Task<List<ProductImages>> GetImagesByProductId(int  productId);
    }
}
