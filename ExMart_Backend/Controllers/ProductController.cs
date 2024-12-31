using AutoMapper;
using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        private DBDataInitializer _dbInitializer;

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
            var products = _dbInitializer.GetProducts();
            return Ok(products);
        }

        [HttpGet("GetProductById")]

        public async Task<IActionResult> GetProductById(int id)
        {
            return Ok(await _dbInitializer.GetProductById(id));
        }

        [HttpPost]
        [Route("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO addProductDTO)
        {
            Product product = _mapper.Map<Product>(addProductDTO);
            if (product == null)
            {
                return BadRequest("Product data is null.");
            }
            try
            {
                var newProduct = await _productRepository.AddProductAsync(product);
                return Ok(newProduct);
            }
            catch
            {
                return StatusCode(500, "An error occurred while adding the product.");
            }
        }

        //[HttpPost]
        //[Route("add-product")] 
        //public async Task<IActionResult> AddProduct([FromBody] Product product) 
        //{ 
        //    if (product == null) 
        //    { 
        //        return BadRequest("Product data is null.");
        //    } 
        //    try 
        //    { 
        //        var newProduct = await _productRepository.AddProductAsync(product);
        //        return Ok(newProduct);
        //    } 
        //    catch 
        //    { 
        //        return StatusCode(500, "An error occurred while adding the product.");
        //    } 
        //}

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
                    message = "An error occurred while updating the product status"
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

            var products = await _productRepository.GetProductsByNameAsync(name);
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }

            return Ok(products);
        }
    }
}
