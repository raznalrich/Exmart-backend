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
        //new 
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
    }
}
