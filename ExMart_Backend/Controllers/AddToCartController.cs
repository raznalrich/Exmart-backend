using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/addtocart")]
    [ApiController]
    public class AddToCartController:ControllerBase
    {
        private IAddToCartRepository _addToCartRepository;
        private DBDataInitializer _dbInitializer;

        public AddToCartController(IAddToCartRepository addToCartRepository,DBDataInitializer dBDataInitializer)
        {
            _addToCartRepository = addToCartRepository;
            _dbInitializer = dBDataInitializer;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddToCart addToCart)
        {
            if (addToCart == null)
            {
                return BadRequest("Invalid cart data.");
            }

            try
            {
                bool isAdded = _addToCartRepository.AddToCart(addToCart);
                if (isAdded)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return BadRequest("Failed to add item to cart.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cart = await _addToCartRepository.GetCartList();
                if (cart == null || !cart.Any())
                {
                    return NoContent();
                }
                else
                {
                    return Ok(cart);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("DeleteCart")] 
        public async Task<IActionResult> DeleteCart(int productId, int userId) 
        {
            if (productId <= 0 || userId <= 0)
            {
                return BadRequest("Invalid productId or userId.");
            }

            try
            {
                var isDeleted = _addToCartRepository.DeleteCartList(productId, userId);
                if (isDeleted)
                {
                    return Ok(new { Message = "Product removed from cart successfully." });
                }
                return NotFound(new { Message = "Product not found in cart." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpDelete("DeleteAllUserCart/{userId}")]
        public async Task<IActionResult> DeleteAllUserCart(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { Message = "Invalid userId." });
            }

            try
            {
                var isDeleted = _addToCartRepository.DeleteAllUserCartItems(userId);
                if (isDeleted)
                {
                    return Ok(new { Message = "All cart items for the user were removed successfully." });
                }
                return NotFound(new { Message = "No cart items found for this user." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
