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

        public async Task<Product> AddProductAsync(Product product)
        {
            product.IsActive = true;
            try
            {
                if (product.ProductImages != null && product.ProductImages.Any())
                {
                    foreach (var image in product.ProductImages)
                    {
                        //image.ProductId = product.Id;
                        image.Product = product;

                    }
                }
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return product;
            }
            catch
            {
                throw new Exception("An error occurred while adding the product.");
            }
        }

        public async Task<bool> DeactivateProductAsync(int id)
        {
            var product =await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            // Toggle the IsActive status
            product.IsActive = !product.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return product.IsActive;
        }

        public Task<Product> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<object>> GetProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return Enumerable.Empty<Product>();
            }

            var products = await _db.Products
                .Where(p => p.Name.ToLower().Contains(productName.ToLower()))
                .ToListAsync();

            return products;
        }


    }
}
