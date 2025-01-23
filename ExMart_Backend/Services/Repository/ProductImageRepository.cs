using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly Supabase.Client _supabaseClient;


        public ProductImageRepository(ApplicationDBContext db, Supabase.Client supabaseClient)
        {
            _db = db;
            _supabaseClient = supabaseClient;

        }
        public async Task<List<ProductImages>> GetImagesByProductId(int productId)
        {
            var images = await _db.Images
                                  .Where(img => img.ProductId == productId)
                                  .ToListAsync();
            return images;
        }
    }
}
