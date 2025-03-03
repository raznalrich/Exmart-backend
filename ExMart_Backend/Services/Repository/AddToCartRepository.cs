using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Migrations;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExMart_Backend.Services.Repository
{
    public class AddToCartRepository : IAddToCartRepository

    {
        private readonly ApplicationDBContext _context;

        public AddToCartRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public AddToCartRepository()
        {
        }

        public bool AddToCart(AddToCart addToCart)
        {
            try
            {
                // Validate input parameters
                if (addToCart == null)
                {
                    throw new ArgumentNullException(nameof(addToCart), "Cart item cannot be null");
                }

                if (addToCart.ProductId <= 0)
                {
                    throw new ArgumentException("Invalid product ID", nameof(addToCart.ProductId));
                }

                if (addToCart.UserId <= 0)
                {
                    throw new ArgumentException("Invalid user ID", nameof(addToCart.UserId));
                }

                if (addToCart.Quantity <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero", nameof(addToCart.Quantity));
                }

                // Check if the item already exists in cart
                var existingItem = _context.CartList.FirstOrDefault(
                    x => x.ProductId == addToCart.ProductId && x.UserId == addToCart.UserId && x.SizeId == addToCart.SizeId && x.ColorId == addToCart.ColorId

                );

                if (existingItem != null)
                {
                    return false;
                }
                //addToCart.CartId = DBDataInitializer.GetNextCartId();
                int latestCartId = _context.CartList.Any() ? _context.CartList.Max(x => x.CartId) : 0;
                addToCart.CartId = latestCartId + 1;
                // Add to cart
                _context.CartList.Add(addToCart);
                _context.SaveChanges(); 
                return true;
            }
            catch (Exception)
            {
                // Log the exception here
                throw;
            }
        }

        public async Task<ICollection<CartListDTO>> GetCartList()
        {
            try
            {
                if (_context.CartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var cartList = _context.CartList;
                var carListDTO = cartList.Select(cartitem => new CartListDTO
                {
                    CartId = cartitem.CartId,
                    ProductId = cartitem.ProductId,
                    Quantity = cartitem.Quantity,
                    ColorId = cartitem.ColorId,
                    SizeId = cartitem.SizeId,
                    ProductName = _context.Products.FirstOrDefault(p => p.Id == cartitem.ProductId).Name,
                    ProductImageUrl = _context.Products.FirstOrDefault(p=>p.Id == cartitem.ProductId).PrimaryImageUrl,
                    Price = _context.Products.FirstOrDefault(p =>p.Id == cartitem.ProductId).Price,
                    ColorName = _context.ColourMaster.FirstOrDefault(p=>p.ColorId == cartitem.ColorId).ColorName,
                    SizeName = _context.SizeMaster.FirstOrDefault(p=>p.SizeId == cartitem.SizeId).Size,
                    UserId = cartitem.UserId,

                }).ToList();
                return await Task.FromResult(
                    carListDTO
                    );
            }
            catch (Exception)
            {
                // Log the exception here
                throw;
            }
        }

        public bool DeleteCartList(int cartId)
        {
            try
            {
                if (cartId <= 0)
                {
                    throw new ArgumentException("Invalid cart ID", nameof(cartId));
                }
                if (_context.CartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var itemToRemove = _context.CartList.FirstOrDefault(
                    cart => cart.CartId == cartId
                );

                if (itemToRemove == null)
                {
                    return false;
                }

                _context.CartList.Remove(itemToRemove);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Log the exception here
                throw;
            }
        }

        public bool DeleteAllUserCartItems(int userId)
        {
            try
            {
                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid user ID", nameof(userId));
                }

                if (_context.CartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var itemsToRemove = _context.CartList
                    .Where(cart => cart.UserId == userId)
                    .ToList();

                if (!itemsToRemove.Any())
                {
                    return false;
                }

                foreach (var item in itemsToRemove)
                {
                    _context.CartList.Remove(item);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                // Log the exception here
                throw;
            }
        }
    }
}