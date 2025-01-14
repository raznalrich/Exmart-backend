using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExMart_Backend.Services.Repository
{
    public class AddToCartRepository : IAddToCartRepository
    {
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
                    x => x.ProductId == addToCart.ProductId && x.UserId == addToCart.UserId
                );

                if (existingItem != null)
                {
                    throw new InvalidOperationException("Item already exists in cart");
                }

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

        public async Task<ICollection<AddToCart>> GetCartList()
        {
            try
            {
                if (DBDataInitializer.cartList == null)
                {
                    throw new InvalidOperationException("Cart list is not initialized");
                }

                var cartList = DBDataInitializer.cartList.ToList();
                return await Task.FromResult(cartList);
            }
            catch (Exception)
            {
                // Log the exception here
                throw;
            }
        }

        public bool DeleteCartList(int productId, int userId)
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
                    cart => cart.ProductId == productId && cart.UserId == userId
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