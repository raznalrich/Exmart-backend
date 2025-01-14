using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IAddToCartRepository
    {
        bool AddToCart(AddToCart AddToCart);
        //List<AddToCart> GetCartList();
        Task<ICollection<CartListDTO>> GetCartList();

        bool DeleteCartList(int productId, int userId);
        bool DeleteAllUserCartItems(int userId);
    }
}
