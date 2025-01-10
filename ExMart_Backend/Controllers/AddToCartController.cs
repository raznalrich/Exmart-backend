using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ExMart_Backend.Controllers
{
    [Route("api/addtocart")]
    [ApiController]
    public class AddToCartController : ControllerBase
    {
        private readonly IAddToCartRepository _addToCartRepository;
        private readonly DBDataInitializer _dbInitializer;

        public AddToCartController(IAddToCartRepository addToCartRepository, DBDataInitializer dbInitializer)
        {
            _addToCartRepository = addToCartRepository ?? throw new ArgumentNullException(nameof(addToCartRepository));
            _dbInitializer = dbInitializer ?? throw new ArgumentNullException(nameof(dbInitializer));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AddToCart addToCart)
        {
            try
            {
                // Validate input
                if (addToCart == null)
                {
                    return BadRequest(new { Message = "Request body cannot be null" });
                }

                // Validate model state
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(new { Message = "Invalid model state", Errors = errors });
                }

                // Business validation
                if (addToCart.ProductId <= 0)
                {
                    return BadRequest(new { Message = "Invalid product ID" });
                }

                if (addToCart.UserId <= 0)
                {
                    return BadRequest(new { Message = "Invalid user ID" });
                }

                if (addToCart.Quantity <= 0)
                {
                    return BadRequest(new { Message = "Quantity must be greater than zero" });
                }

                bool isAdded = await Task.Run(() => _addToCartRepository.AddToCart(addToCart));

                if (isAdded)
                {
                    return StatusCode(StatusCodes.Status201Created,
                        new { Message = "Item added to cart successfully" });
                }

                return BadRequest(new { Message = "Failed to add item to cart" });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while processing your request" });
            }
        }

        [HttpGet("GetCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCart()
        {
            try
            {
                var cart = await _addToCartRepository.GetCartList();

                if (cart == null || !cart.Any())
                {
                    return NoContent();
                }

                return Ok(new { Items = cart, Count = cart.Count });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while retrieving cart items" });
            }
        }

        [HttpDelete("DeleteCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCart([Range(1, int.MaxValue)] int productId, [Range(1, int.MaxValue)] int userId)
        {
            try
            {
                if (productId <= 0)
                {
                    return BadRequest(new { Message = "Product ID must be greater than zero" });
                }

                if (userId <= 0)
                {
                    return BadRequest(new { Message = "User ID must be greater than zero" });
                }

                var isDeleted = await Task.Run(() => _addToCartRepository.DeleteCartList(productId, userId));

                if (isDeleted)
                {
                    return Ok(new { Message = "Product removed from cart successfully" });
                }

                return NotFound(new { Message = "Product not found in cart" });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while removing the item from cart" });
            }
        }

        [HttpDelete("DeleteAllUserCart/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllUserCart([Range(1, int.MaxValue)] int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return BadRequest(new { Message = "User ID must be greater than zero" });
                }

                var isDeleted = await Task.Run(() => _addToCartRepository.DeleteAllUserCartItems(userId));

                if (isDeleted)
                {
                    return Ok(new { Message = "All cart items for the user were removed successfully" });
                }

                return NotFound(new { Message = "No cart items found for this user" });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "An error occurred while removing user's cart items" });
            }
        }
    }
}