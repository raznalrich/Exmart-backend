namespace ExMart_Backend.DTO
{
    public class OrderItemListDTO
    {
        public int OrderItemId { get; set; }

        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public string? PrimaryImageUrl { get; set; }

        public int Status { get; set; }

        public decimal Amount { get; set; }

        public int Quantity { get; set; }
        public int OrderId { get; set; }
    }
}
