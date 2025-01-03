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
        //private const string APP_PREFIX = "EXMART";

        public OrderRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        //public async Task<string> GenerateOrderId()
        //{
        //    var now = DateTime.UtcNow;
        //    var year = now.ToString("yyyy");
        //    var month = now.ToString("MM");

        //    Get the last order number for the current month

        //   var lastOrder = await _db.Orders
        //       .Where(o => o.OrderId.StartsWith($"{APP_PREFIX}-{year}-{month}"))
        //       .OrderByDescending(o => o.OrderId)
        //       .FirstOrDefaultAsync();

        //    int sequence = 1;
        //    if (lastOrder != null)
        //    {
        //        // Extract the sequence number from the last order ID
        //        var lastSequence = int.Parse(lastOrder.OrderId.Split('-').Last());
        //        sequence = lastSequence + 1;
        //    }

        //    return $"{APP_PREFIX}-{year}-{month}-{sequence:D4}";
        //}
        public async Task<Order> AddOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order.OrderItems == null || !order.OrderItems.Any())
            {
                throw new ArgumentException("Order must contain at least one item");
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // Detach any existing entities to prevent tracking issues
                foreach (var entry in _db.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }

                // Generate OrderId
                var lastOrderId = await _db.Orders
                    .MaxAsync(o => (int?)o.OrderId) ?? 0;
                order.OrderId = lastOrderId + 1;

                // Add the order
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();

                // Assign a new OrderItemId by getting the last one and adding 1
                var lastOrderItemId = await _db.OrderItems
                                                  .OrderByDescending(item => item.OrderItemId)
                                                  .Select(item => item.OrderItemId)
                                                  .FirstOrDefaultAsync();

                // Clear existing OrderItems collection and add fresh items
                var orderItems = order.OrderItems.ToList();
                order.OrderItems.Clear();

                foreach (var item in orderItems)
                {
                    lastOrderItemId++;
                    var newItem = new OrderItem
                    {
                        OrderItemId = lastOrderItemId,
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SizeId = item.SizeId,
                        ColorId = item.ColorId
                    };
                    await _db.OrderItems.AddAsync(newItem);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Order> GetOrderWithDetails(int orderId)
        {
            return await _db.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .Include(o => o.ProductStatus)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
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

        public async Task<List<OrderListDTO>> GetOrderToList()
        {
            return await _db.Orders
                .Include(o => o.User)
                .Include(o => o.ProductStatus)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Select(o => new OrderListDTO
                {
                    OrderId = o.OrderId,
                    OrderDate = o.CreatedAt,
                    CustomerName = o.User.Name,

                    Status = o.ProductStatus.Product_StatusId,

                    UserId = o.User.Id,

                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price),
                    TotalItems = o.OrderItems.Sum(oi => oi.Quantity)
                })
                .ToListAsync();
        }

        async Task<OrderDetailByOrderIdDTO> IOrderRepository.GetOrderDetailsById(int orderId)
        {
            return await _db.Orders.Where(o => o.OrderId == orderId).Select(o => new OrderDetailByOrderIdDTO
            {
                Name = o.User.Name,
                Email = o.User.Email,
                Phone = o.User.Phone,
                OrderId = o.OrderId,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price),
                AddressLine = o.UserAddress.AddressLine,
                City = o.UserAddress.City,
                State = o.UserAddress.State,
                ZipCode = o.UserAddress.ZipCode,
                OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    SizeName = oi.Size.Size,
                    ColorName = oi.Color.ColorName,
                    Price = oi.Product.Price,
                    SubTotal = oi.Quantity * oi.Product.Price
                }).ToList()
            }).FirstOrDefaultAsync();
        }

        public async Task<object>UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            {
                var order = await _db.Orders
                    .Include(o => o.ProductStatus)
                    .FirstOrDefaultAsync(o => o.OrderId == request.OrderId);

                if (order == null)
                {
                    throw new Exception($"Order with ID {request.OrderId} not found");
                }

                var newStatus = await _db.StatusMaster
                    .FirstOrDefaultAsync(s => s.Product_StatusId == request.ProductStatusId);

                if (newStatus == null)
                {
                    throw new Exception($"Status with ID {request.ProductStatusId} not found");
                }

                order.Product_StatusId = request.ProductStatusId;

                await _db.SaveChangesAsync();

                return order;
               
            }
        }
    }
}
