using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IOrderRepository
    {
        Task<Order> AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();

        Task<Order> GetOrderById(int id);
    }
}
