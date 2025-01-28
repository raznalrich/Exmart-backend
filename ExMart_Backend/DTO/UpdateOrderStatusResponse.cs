namespace ExMart_Backend.DTO
{
    public class UpdateOrderStatusResponse
    {
        public int OrderItemId { get; set; }
        public int ProductStatusId { get; set; }
        public int? shippingCharge { get; set; }
    }
}