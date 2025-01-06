namespace ExMart_Backend.DTO
{
    public class UpdateOrderStatusRequest
    {
        public int OrderItemId { get; set; }
        public int ProductStatusId { get; set; }
    }
}
