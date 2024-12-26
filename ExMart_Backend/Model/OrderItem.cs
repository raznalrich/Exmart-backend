namespace ExMart_Backend.Model
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        public int ProductRateId { get; set; }

        public int SizeId { get; set; }

        public int ColorId { get; set; }

        // Foreign key to the related Order
        public int OrderId { get; set; }
    }
}
