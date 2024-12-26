using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;

namespace ExMart_Backend.Services.Repository
{
    public class AddToCartRepository : IAddToCartRepository
    {
        public bool AddToCart(AddToCart addToCart)
        {
            DBDataInitializer.cartList.Add(addToCart);
            return true;
        }

        //public Task<ICollection<AddToCart>> GetCartList()
        //{
        //    List<AddToCart> carts = DBDataInitializer.cartList.ToListasync();
        //    return carts;
        //}
        public async Task<ICollection<AddToCart>> GetCartList()
        {
            return DBDataInitializer.cartList.ToList();
        }
    }
}
