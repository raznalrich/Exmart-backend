using System.Net;
using AutoMapper;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("placeorder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDTO placeOrderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //var orderId = await _orderRepository.GenerateOrderId();
                // Explicitly create new Order without using AutoMapper
                var order = new Order
                {
                    //OrderId = orderId,
                    UserId = placeOrderDTO.UserId,
                    UserAddressId = placeOrderDTO.AddressId,
                    Product_StatusId = 1,
                    CreatedAt = DateTime.UtcNow,
                    // Initialize a new list for OrderItems
                    OrderItems = new List<OrderItem>()
                };

                // Explicitly create OrderItems
                foreach (var itemDTO in placeOrderDTO.OrderItems)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = itemDTO.ProductId,
                        Quantity = itemDTO.Quantity,
                        SizeId = itemDTO.SizeId,
                        ColorId = itemDTO.ColorId
                    };
                    order.OrderItems.Add(orderItem);
                }

                var result = await _orderRepository.AddOrder(order);

                //return Ok(result);
                // Load and return the complete order with details

                var orderWithDetails = await _orderRepository.GetOrderWithDetails(result.OrderId);
                return CreatedAtAction(nameof(_orderRepository.GetOrderById), new { id = orderWithDetails.OrderId }, orderWithDetails);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    message = "Database error occurred while placing the order",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while processing your order",
                    error = ex.Message
                });
            }
        }


        [HttpGet]
        [Route("getallorders")]
        public async Task<IActionResult> GetAllOrders()
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrders();
            return Ok(orders);
        }

        [HttpGet]
        [Route("getorderbyid/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            return Ok(order);
        }

        [HttpGet("orders/List")]
        public async Task<IActionResult> GetOrderDetails()
        {
            try
            {
                var orderList = await _orderRepository.GetOrderToList();
                return Ok(orderList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving order List", error = ex.Message });
            }
        }

        [HttpGet("orders/detailsbyid/{orderid}")] 
        public async Task<IActionResult> GetOrderDetailById(int orderid)
        {
            try
            {
                OrderDetailByOrderDTO orderDetails = await _orderRepository.GetOrderDetailsById(orderid);
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving order details", error = ex.Message });
            }
        }

            [HttpPut("updatestatus")]
            public async Task<IActionResult> UpdateOrderStatus( [FromBody] UpdateOrderStatusRequest request)
            {
                try
                {
                    var result = await _orderRepository.UpdateOrderStatus(request);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while updating the order status");
                }
            }
        
    }
}
