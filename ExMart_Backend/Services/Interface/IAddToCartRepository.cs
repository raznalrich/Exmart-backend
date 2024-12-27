using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IAddToCartRepository
    {
        bool AddToCart(AddToCart AddToCart);
        //List<AddToCart> GetCartList();
        Task<ICollection<AddToCart>> GetCartList();


    }
}
