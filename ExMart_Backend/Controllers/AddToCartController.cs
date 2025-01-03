﻿using System.Threading.Tasks;
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
            bool isAdded = _addToCartRepository.AddToCart(addToCart);
            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            List<AddToCart> cart = (List<AddToCart>)await _addToCartRepository.GetCartList();
            if (cart == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(cart);
            }            
        }

        [HttpDelete("DeleteCart")] 
        public async Task<IActionResult> 
            DeleteCart(int productId, int userId) { 
            if (productId <= 0 || userId <= 0) 
            { 
                return BadRequest("Invalid productId or userId.");
            } 
            var isDeleted = _addToCartRepository.DeleteCartList(productId, userId);
            if (isDeleted) { 
                return Ok(new {
                    Message = "Product removed from cart successfully." });
            } 
            return NotFound(new { Message = "Product not found in cart." });
        }
    }
}
