using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;

namespace ExMart_Backend.Services.Repository
{
    public class AddToCartRepository : IAddToCartRepository
    {
        public bool AddToCart(AddToCart addToCart)
        {
            try
            {
                DBDataInitializer.cartList.Add(addToCart);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<ICollection<AddToCart>> GetCartList()
        {
            try
            {
                return DBDataInitializer.cartList.ToList();
            }
            catch (Exception ex)
            {
                return new List<AddToCart>();
            }
        }
        public bool DeleteCartList(int productId, int userId)
        {
            try
            {
                var itemToRemove = DBDataInitializer.cartList.FirstOrDefault(cart => cart.ProductId == productId && cart.UserId == userId);
                if (itemToRemove != null)
                {
                    DBDataInitializer.cartList.Remove(itemToRemove);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteAllUserCartItems(int userId)
        {
            try
            {
                var itemsToRemove = DBDataInitializer.cartList.Where(cart => cart.UserId == userId).ToList();
                if (itemsToRemove.Any())
                {
                    foreach (var item in itemsToRemove)
                    {
                        DBDataInitializer.cartList.Remove(item);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
