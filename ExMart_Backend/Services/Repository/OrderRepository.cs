using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _db;

        public OrderRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<Order> AddOrder(Order order)
        {
            // Validate the order object
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }
            // Ensure required fields are set
            if (order.UserId <= 0)
            {
                throw new ArgumentException("Invalid User ID.", nameof(order.UserId));
            }

            if (order.AddressId <= 0)
            {
                throw new ArgumentException("Invalid Address ID.", nameof(order.AddressId));
            }

            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new ArgumentException("Order must contain at least one order item.", nameof(order.OrderItems));
            }

            // Validate each order item
            foreach (var orderItem in order.OrderItems)
            {
                orderItem.OrderItemId = await _db.OrderItems
                                              .OrderByDescending(item => item.OrderItemId)
                                              .Select(item => item.OrderItemId)
                                              .FirstOrDefaultAsync() + 1;
                if (orderItem.ProductId <= 0)
                {
                    throw new ArgumentException("Invalid Product ID in order item.");
                }

                if (orderItem.ProductRateId <= 0)
                {
                    throw new ArgumentException("Invalid Product Rate ID in order item.");
                }

                if (orderItem.SizeId <= 0)
                {
                    throw new ArgumentException("Invalid Size ID in order item.");
                }

                if (orderItem.ColorId <= 0)
                {
                    throw new ArgumentException("Invalid Color ID in order item.");
                }
            }
            order.OrderId = await _db.Orders
                                     .OrderByDescending(o => o.OrderId)
                                     .Select(o => o.OrderId)
                                     .FirstOrDefaultAsync() + 1;
            order.CreatedAt = DateTime.UtcNow;


            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            Order order = await _db.Orders.Include(o => o.OrderItems)
                          .FirstOrDefaultAsync(o => o.OrderId == id);

            return order;
        }
    }
}
