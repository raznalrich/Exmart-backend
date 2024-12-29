using System.Net;
using AutoMapper;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

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

            // Map the PlaceOrderDTO to Order entity
            var order = _mapper.Map<Order>(placeOrderDTO);

            // Map OrderItems from PlaceOrderDTO to OrderItem entities
            var orderItems = new List<OrderItem>();
            foreach (var itemDTO in placeOrderDTO.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    ProductRateId = itemDTO.ProductRateId,
                    SizeId = itemDTO.SizeId,
                    ColorId = itemDTO.ColorId,
                    Quantity = 1 // Default quantity, you may adjust this as needed
                };
                orderItems.Add(orderItem);
            }

            // Assign the OrderItems to the Order entity
            order.OrderItems = orderItems;

            // Set other required properties for the order, such as Product_StatusId and CreatedAt
            //order.Product_StatusId = placeOrderDTO.Product_StatusId;
            order.CreatedAt = DateTime.UtcNow;

            try
            {
                // Call the AddOrder function in the repository
                Order resOrder = await _orderRepository.AddOrder(order);

                // Return a successful response with the created order
                return Ok(resOrder);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during order creation
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error placing order: {ex.Message}");
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
    }
}
