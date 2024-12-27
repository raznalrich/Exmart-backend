using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDBContext _db;

        public ProductImageRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<List<ProductImage>> GetImagesByProductId(int productId)
        {
            var images = await _db.ProductImages
                                  .Where(img => img.ProductId == productId)
                                  .ToListAsync();
            return images;
        }
    }
}
