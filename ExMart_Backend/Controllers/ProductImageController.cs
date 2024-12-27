using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageController(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }
        [HttpGet("ByProduct/{productId}")]
        public async Task<IActionResult> GetImagesByProductId(int productId)
        {
            return Ok( await _productImageRepository.GetImagesByProductId(productId));
        }
    }
}
