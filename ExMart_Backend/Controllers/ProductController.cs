using ExMart_Backend.Data;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly DBDataInitializer _dbInitializer;
        public ProductController(IProductRepository productRepository, DBDataInitializer dbInitializer)
        {
            _productRepository = productRepository;
            _dbInitializer = dbInitializer;
        }
        public IActionResult GetAllProducts()
        {
            var products = _dbInitializer.GetProducts();
            return Ok(products);
        }
    }
}
