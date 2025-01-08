using ExMart_Backend.Data;
using ExMart_Backend.DTO;
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

            product.IsActive = !product.IsActive;
            product.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return product.IsActive;
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == productId);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(); 
            return product;
        }

        public async Task<Product> GetProductById(int id)
        {
            Product product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            return product;
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


        public async Task<Product> UpdateProductAsync(int productId, EditProductDTO productDTO)
        {
            try
            {
                var existingProduct = await _db.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == productId);

                if (existingProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {productId} not found.");
                }
                // Update product fields
                existingProduct.Name = productDTO.Name;
                existingProduct.Description = productDTO.Description;
                existingProduct.Brand = productDTO.Brand;
                existingProduct.VendorId = productDTO.VendorId;
                existingProduct.CategoryId = productDTO.CategoryId;
                existingProduct.PrimaryImageUrl = productDTO.PrimaryImageUrl;
                existingProduct.Weight = productDTO.Weight;
                existingProduct.Price = productDTO.Price;
                existingProduct.UpdatedAt = DateTime.UtcNow;
                if (productDTO.ProductImages != null && productDTO.ProductImages.Any())
                {
                    // 1) Clear out the old images
                    _db.Images.RemoveRange(existingProduct.ProductImages);
                    existingProduct.ProductImages.Clear();

                    // 2) Add brand-new images from the DTO
                    foreach (var dtoImg in productDTO.ProductImages)
                    {
                        var newImage = new ProductImages
                        {
                            ImageUrl = dtoImg.ImageUrl,
                            ProductId = existingProduct.Id
                        };
                        _db.Images.Add(newImage);
                        // or existingProduct.ProductImages.Add(newImage); 
                        // either works if relationships are set properly
                    }
                }
                else
                {
                    // If no images were sent, you might want to remove them all or do nothing.
                    _db.Images.RemoveRange(existingProduct.ProductImages);
                    existingProduct.ProductImages.Clear();
                }


                await _db.SaveChangesAsync();
                return existingProduct;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the product: {ex.Message}", ex);
            }
        }
    }
}
