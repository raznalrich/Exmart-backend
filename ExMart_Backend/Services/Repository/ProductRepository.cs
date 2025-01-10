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
                        image.Product = product;
                    }
                }

                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding the product: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeactivateProductAsync(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                product.IsActive = !product.IsActive;
                product.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();

                return product.IsActive;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deactivating the product: {ex.Message}", ex);
            }
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(u => u.Id == productId);

                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {productId} not found.");
                }

                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

                return product;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the product: {ex.Message}", ex);
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found.");
                }

                return product;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the product: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<object>> GetProducts()
        {
            try
            {
                return await _db.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving products: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string productName)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving products by name: {ex.Message}", ex);
            }
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
                    // Remove old images
                    _db.Images.RemoveRange(existingProduct.ProductImages);

                    // Add new images from the DTO
                    foreach (var dtoImg in productDTO.ProductImages)
                    {
                        var newImage = new ProductImages
                        {
                            ImageUrl = dtoImg.ImageUrl,
                            ProductId = existingProduct.Id
                        };

                        _db.Images.Add(newImage);
                    }
                }
                else
                {
                    // Clear all images if none are provided
                    _db.Images.RemoveRange(existingProduct.ProductImages);
                }

                await _db.SaveChangesAsync();
                return existingProduct;
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the product: {ex.Message}", ex);
            }
        }

    }
}
