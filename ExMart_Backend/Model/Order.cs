namespace ExMart_Backend.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Order_ItemId { get; set; }
        public int AddressId { get; set; }
        public int Product_StatusId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
