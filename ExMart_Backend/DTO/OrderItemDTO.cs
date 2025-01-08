namespace ExMart_Backend.DTO
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductImageUrl { get; set; }
        public int Product_StatusId { get; set; }
        public int Quantity { get; set; }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int? shippingCharge { get; set; }
        public decimal Price { get; set; }

        public decimal SubTotal { get; set; }
    }
}
