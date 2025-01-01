using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IOrderRepository
    {
        //Task<string> GenerateOrderId();
        Task<Order> AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);

        Task<Order> GetOrderWithDetails(int orderId);

        Task<List<OrderListDTO>> GetOrderDetails();

        Task<object> UpdateOrderStatus(UpdateOrderStatusRequest request);
    }
}
