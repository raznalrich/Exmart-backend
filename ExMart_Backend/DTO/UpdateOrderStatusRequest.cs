namespace ExMart_Backend.DTO
{
    public class UpdateOrderStatusRequest
    {
        public int OrderId { get; set; }
        public int ProductStatusId { get; set; }
    }
}
