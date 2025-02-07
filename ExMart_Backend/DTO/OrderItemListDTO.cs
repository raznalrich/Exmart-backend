namespace ExMart_Backend.DTO
{
    public class OrderItemListDTO
    {
        public int OrderItemId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public int Status { get; set; }
        public decimal Amount { get; set; }
        public decimal Product_amount { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int? ShippingCharge { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public int UserId { get; set; }
    }
}
