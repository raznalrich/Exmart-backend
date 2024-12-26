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
            Order order = _mapper.Map<Order>(placeOrderDTO);

            Order resorder = await _orderRepository.AddOrder(order);
            return Ok(resorder);
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
