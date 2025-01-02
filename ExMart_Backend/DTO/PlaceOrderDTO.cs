namespace ExMart_Backend.DTO
{
    public class PlaceOrderDTO
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
        
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
