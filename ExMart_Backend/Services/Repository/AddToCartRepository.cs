using ExMart_Backend.Data;
using ExMart_Backend.DTO;
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
                var existingItem = DBDataInitializer.cartList.FirstOrDefault(
                    x => x.ProductId == addToCart.ProductId && x.UserId == addToCart.UserId && x.SizeId == addToCart.SizeId && x.ColorId == addToCart.ColorId

                );

                if (existingItem != null)
                {
                    return false;
                }
                addToCart.CartId = DBDataInitializer.GetNextCartId();

                // Add to cart
                DBDataInitializer.cartList.Add(addToCart);
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
                if (DBDataInitializer.cartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var cartList = DBDataInitializer.cartList;
                var carListDTO = cartList.Select(cartitem => new CartListDTO
                {
                    ProductId = cartitem.ProductId,
                    Quantity = cartitem.Quantity,
                    ColorId = cartitem.ColorId,
                    SizeId = cartitem.SizeId,
                    ProductName = _context.Products.FirstOrDefault(p => p.Id == cartitem.ProductId)?.Name,
                    ProductImageUrl = _context.Products.FirstOrDefault(p=>p.Id == cartitem.ProductId)?.PrimaryImageUrl,
                    Price = _context.Products.FirstOrDefault(p =>p.Id == cartitem.ProductId).Price,
                    ColorName = _context.ColourMaster.FirstOrDefault(p=>p.ColorId == cartitem.ColorId)?.ColorName,
                    SizeName = _context.SizeMaster.FirstOrDefault(p=>p.SizeId == cartitem.SizeId)?.Size,
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

        public bool DeleteCartList(int productId, int userId , int colorId,int sizeId)
        {
            try
            {
                if (productId <= 0)
                {
                    throw new ArgumentException("Invalid product ID", nameof(productId));
                }

                if (userId <= 0)
                {
                    throw new ArgumentException("Invalid user ID", nameof(userId));
                }

                if (DBDataInitializer.cartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var itemToRemove = DBDataInitializer.cartList.FirstOrDefault(
                    cart => cart.ProductId == productId && cart.UserId == userId && cart.ColorId == colorId && cart.SizeId == sizeId
                );

                if (itemToRemove == null)
                {
                    return false;
                }

                DBDataInitializer.cartList.Remove(itemToRemove);
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

                if (DBDataInitializer.cartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var itemsToRemove = DBDataInitializer.cartList
                    .Where(cart => cart.UserId == userId)
                    .ToList();

                if (!itemsToRemove.Any())
                {
                    return false;
                }

                foreach (var item in itemsToRemove)
                {
                    DBDataInitializer.cartList.Remove(item);
                }

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