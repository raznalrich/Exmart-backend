using System.Drawing;
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
        public async Task<OrderResponseDTO> AddOrder(Order order, int shippingCharge)
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



                var lastOrderItemId = await _db.OrderItems
                               .MaxAsync(item => item.OrderItemId);

                // Clear existing OrderItems collection and add fresh items
                var orderItems = order.OrderItems.ToList();
                order.OrderItems.Clear();

                foreach (var item in orderItems)
                {

                    var newItem = new OrderItem
                    {

                        OrderId = order.OrderId,
                        Product_StatusId = item.Product_StatusId,
                        ProductId = item.ProductId,
                        shippingCharge = shippingCharge,
                        Quantity = item.Quantity,
                        SizeId = item.SizeId,
                        ColorId = item.ColorId
                    };

                    await _db.OrderItems.AddAsync(newItem);
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                // Fetch the saved order with related data
                var completedOrder = await _db.Orders
                    .Include(o => o.User)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                    .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

                if (completedOrder == null)
                {
                    throw new InvalidOperationException("Failed to retrieve the saved order");
                }



                // Map to OrderResponseDTO
                var responseDTO = new OrderResponseDTO
                {
                    OrderId = completedOrder.OrderId,
                    UserName = completedOrder.User?.Name ?? "N/A",
                    Email = completedOrder.User?.Email ?? "N/A",
                    CreatedAt = completedOrder.CreatedAt ?? DateTime.Now,
                    OrderItems = completedOrder.OrderItems.Select(item => new OrderItemResponseDTO
                    {
                        OrderItemId = item.OrderItemId,
                        ProductName = item.Product?.Name ?? "Unknown Product",
                        Quantity = item.Quantity,
                        shippingCharge = item.shippingCharge,
                        Price = item.Product?.Price ?? 0,
                    }).ToList()
                };

                return responseDTO;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Order> GetOrderWithDetails(int orderId)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Order ID must be greater than zero.", nameof(orderId));
            }
            try
            {
               var order = await _db.Orders
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {orderId} was not found.");
                }

                return order;
            }
            catch(Exception ex) 
            {
                    Console.Error.WriteLine($"An error occurred while fetching the order: {ex.Message}");
                    // Optionally rethrow or handle it
                    throw new ApplicationException("An unexpected error occurred while fetching the order details.", ex);
            }
            
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                return await _db.Orders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occured while retrieving the orders. Please try again later", ex);

            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Order Id must be a positive number.", nameof(id));
            }
            
            try
            {
                Order order = await _db.Orders.Include(o => o.OrderItems)
                         .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {id} was not found. ");
                }

                return order;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while fetching the order details.", ex);
            }
           
           
            
        }

        public async Task<List<OrderListDTO>> GetOrderToList()
        {
            try
            {
                return await _db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Select(o => new OrderListDTO
                {
                    OrderId = o.OrderId,
                    OrderDate = o.CreatedAt,
                    CustomerId = o.User.Id,
                    CustomerName = o.User.Name,

                    UserId = o.User.Id,

                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price),
                    TotalItems = o.OrderItems.Sum(oi => oi.Quantity)
                })
                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred while fetching the order details.", ex);
            }
            
        }

        async Task<List<OrderItemListDTO>> IOrderRepository.GetOrderItemToList()
        {
            try
            {
                return await _db.OrderItems.Include(o => o.Order).Include(o => o.Product).OrderBy(o => o.Order.CreatedAt).Select(o => new OrderItemListDTO
                {


                    OrderItemId = o.OrderItemId,
                    OrderDate = o.Order.CreatedAt,
                    ProductName = o.Product.Name,
                    ProductId = o.Product.Id,
                    PrimaryImageUrl = o.Product.PrimaryImageUrl,
                    Status = o.Product_StatusId,
                    Amount = o.Product.Price * o.Quantity,
                    Quantity = o.Quantity,
                    OrderId = o.OrderId,
                    ShippingCharge = o.shippingCharge,
                    UserId = o.Order.UserId,
                }).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occured while fetching the order details.", ex);
            }
           
        }

        async Task<OrderDetailByOrderIdDTO> IOrderRepository.GetOrderDetailsById(int orderId)
        {
            if(orderId <= 0)
            {
                throw new ArgumentException("Order Id must be a Positive number");
            }

            try
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
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product.Name,
                        ProductImageUrl = oi.Product.PrimaryImageUrl,
                        Product_StatusId = oi.Product_StatusId,
                        Quantity = oi.Quantity,
                        SizeName = oi.Size.Size,
                        ColorName = oi.Color.ColorName,
                        shippingCharge = oi.shippingCharge,
                        Price = oi.Product.Price,
                        SubTotal = oi.Quantity * oi.Product.Price
                    }).ToList()
                }).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occured while fetching the order details");
            }
           
        }

        public async Task<UpdateOrderStatusResponse> UpdateOrderStatus(UpdateOrderStatusRequest request)
        {

            
                var orderItem = await _db.OrderItems
                    .Include(o => o.ProductStatus)
                    .FirstOrDefaultAsync(o => o.OrderItemId == request.OrderItemId);

                if (orderItem == null)
                {
                    throw new Exception($"Order with ID {request.OrderItemId} not found");
                }

                var newStatus = await _db.StatusMaster
                    .FirstOrDefaultAsync(s => s.Product_StatusId == request.ProductStatusId);

                if (newStatus == null)
                {
                    throw new Exception($"Status with ID {request.ProductStatusId} not found");
                }

                orderItem.Product_StatusId = request.ProductStatusId;
            orderItem.shippingCharge =      request.shippingCharge;

            var UpdatedStatus = new UpdateOrderStatusResponse
            {
                OrderItemId = orderItem.OrderItemId,
                ProductStatusId = orderItem.Product_StatusId,
                shippingCharge = orderItem.shippingCharge,
            };

                await _db.SaveChangesAsync();

                return UpdatedStatus;
 
        }

       public async Task<int> UpdateOrderStatusByIdOnly(int orderitemid)
        {
            var orderItem = await _db.OrderItems
                     .Include(o => o.ProductStatus)
                     .FirstOrDefaultAsync(o => o.OrderItemId == orderitemid);

            if (orderItem == null)
            {
                throw new Exception($"Order with ID {orderitemid} not found");
            }

            switch (orderItem.Product_StatusId)
            {
                case 1:
                    orderItem.Product_StatusId = 2;
                    break;

                case 2:
                    orderItem.Product_StatusId = 3;
                    break;

                default:
                    throw new Exception($"Order with ID {orderitemid} is already delivered");
            }

            await _db.SaveChangesAsync();

            return orderItem.Product_StatusId;


        }

        public async Task<int> CancelOrderStatusByIdOnly(int orderitemid)
        {
            var orderItem = await _db.OrderItems
                     .Include(o => o.ProductStatus)
                     .FirstOrDefaultAsync(o => o.OrderItemId == orderitemid);

            if (orderItem == null)
            {
                throw new Exception($"Order with ID {orderitemid} not found");
            }
            orderItem.Product_StatusId = 4;

           

            await _db.SaveChangesAsync();

            return orderItem.Product_StatusId;


        }
        public async Task<int> RequestCancelOrderStatusByIdOnly(int orderitemid)
        {
            var orderItem = await _db.OrderItems
                     .Include(o => o.ProductStatus)
                     .FirstOrDefaultAsync(o => o.OrderItemId == orderitemid);

            if (orderItem == null)
            {
                throw new Exception($"Order with ID {orderitemid} not found");
            }
            orderItem.Product_StatusId = 5;



            await _db.SaveChangesAsync();

            return orderItem.Product_StatusId;


        }


    }
}