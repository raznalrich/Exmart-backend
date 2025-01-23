using AutoMapper;
using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private DBDataInitializer _dbInitializer;
        private ProductRepository repository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, DBDataInitializer dbInitializer, IMapper mapper)
        {

            _productRepository = productRepository;
            _dbInitializer = dbInitializer;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _dbInitializer.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while retrieving products: {ex.Message}"
                });
            }
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _dbInitializer.GetProductById(id);
                if (product == null)
                {
                    return NotFound($"Product with ID {id} not found.");
                }

                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while retrieving the product: {ex.Message}"
                });
            }
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO addProductDTO)
        {
            if (addProductDTO == null)
            {
                return BadRequest("Product data is null.");
            }

            try
            {
                var newProduct = await _productRepository.AddProductAsync(addProductDTO);
                return Ok(new { success = true, message = "Product added successfully.", data = newProduct });
                //Product product = _mapper.Map<Product>(addProductDTO);
                //var newProduct = await _productRepository.AddProductAsync(product);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while adding the product: {ex.Message}"
                });
            }
        }

        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleProductStatus(int id)
        {
            try
            {
                var newStatus = await _productRepository.DeactivateProductAsync(id);
                return Ok(new
                {
                    success = true,
                    message = $"Product status successfully updated to {(newStatus ? "active" : "inactive")}"
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while updating the product status: {ex.Message}"
                });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetProductsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Product name cannot be empty.");
            }

            try
            {
                var products = await _productRepository.GetProductsByNameAsync(name);
                if (products == null || !products.Any())
                {
                    return NotFound("No products found.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while searching for products: {ex.Message}"
                });
            }
        }

        [HttpPut("Update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] EditProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Product data cannot be null.");
            }

            try
            {
                var updatedProduct = await _productRepository.UpdateProductAsync(productId, productDTO);
                return Ok(updatedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while updating the product: {ex.Message}"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var deletedProduct = await _productRepository.DeleteProductAsync(id);
                return Ok(deletedProduct);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An error occurred while deleting the product: {ex.Message}"
                });
            }
        }
    }
}
