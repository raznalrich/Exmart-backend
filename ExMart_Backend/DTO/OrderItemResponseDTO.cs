namespace ExMart_Backend.DTO
{
    public class OrderItemResponseDTO
    {
        public int OrderItemId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int? shippingCharge { get; set; }
        public decimal Price { get; set; }
    }
}
