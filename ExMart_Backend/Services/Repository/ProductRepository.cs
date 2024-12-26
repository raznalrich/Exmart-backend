using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _db;

        public ProductRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<object>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
