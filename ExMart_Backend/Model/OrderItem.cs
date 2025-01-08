using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        public int ProductId { get; set; }

        public int SizeId { get; set; }

        public int ColorId { get; set; }

        // Foreign key to the related Order
        public int OrderId { get; set; }
        public int Product_StatusId { get; set; }

        public int? shippingCharge { get; set; }

        public int Quantity { get; set; } // Quantity of the product in this order item

        // Navigation Properties
        public Product Product { get; set; }
        public SizeMaster Size { get; set; }
        public ColourMaster Color { get; set; }
        public Order Order { get; set; }

        public StatusMaster ProductStatus { get; set; }

    }
}
