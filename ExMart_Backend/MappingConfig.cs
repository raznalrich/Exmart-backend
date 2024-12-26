using AutoMapper;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Order, PlaceOrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
        }
    }
}
