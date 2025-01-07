using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IOrderRepository
    {
        //Task<string> GenerateOrderId();
        Task<OrderResponseDTO> AddOrder(Order order,int shippingCharge);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrderById(int id);

        Task<Order> GetOrderWithDetails(int orderId);

        Task<List<OrderListDTO>> GetOrderToList();

        Task<List<OrderItemListDTO>> GetOrderItemToList();

        Task<UpdateOrderStatusResponse> UpdateOrderStatus(UpdateOrderStatusRequest request);

        Task<object> UpdateOrderStatusByIdOnly(int orderitemid);

        Task<OrderDetailByOrderIdDTO> GetOrderDetailsById(int orderId);

    }
}
