using ExMart_Backend.Data;
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
        public ProductController(IProductRepository productRepository, DBDataInitializer dbInitializer)
        {
            _productRepository = productRepository;
            _dbInitializer = dbInitializer;
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
    }
}
