using ExMart_Backend.Data;
using ExMart_Backend.DTO;
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
            // Validate if the order is null before proceeding
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            // Ensure OrderItems are present and valid
            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new ArgumentException("Order must contain at least one order item.", nameof(order.OrderItems));
            }

            // Assign OrderId and other necessary properties if needed
            order.OrderId = await _db.Orders
                                    .OrderByDescending(o => o.OrderId)
                                    .Select(o => o.OrderId)
                                    .FirstOrDefaultAsync() + 1;

            // Set the CreatedAt property
            order.CreatedAt = DateTime.UtcNow;
<<<<<<< HEAD

            // Save the Order to the database
            await _db.Orders.AddAsync(order);
=======
            _db.Orders.Add(order);
>>>>>>> 05a26b99e5e72480244a2ecd399c7d4405112596
            await _db.SaveChangesAsync();

            // Assign OrderItemIds for each OrderItem and save them
            foreach (var orderItem in order.OrderItems)
            {
                // Assign a new OrderItemId by getting the last one and adding 1
                orderItem.OrderItemId = await _db.OrderItems
                                                  .OrderByDescending(item => item.OrderItemId)
                                                  .Select(item => item.OrderItemId)
                                                  .FirstOrDefaultAsync() + 1;
                orderItem.OrderId = order.OrderId; // Set the OrderId for each OrderItem

                // Save the OrderItem to the database
                await _db.OrderItems.AddAsync(orderItem);
            }

            // Save changes for OrderItems
            await _db.SaveChangesAsync();

            // Return the saved Order with associated OrderItems
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
