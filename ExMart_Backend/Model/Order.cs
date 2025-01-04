using System.Net;

namespace ExMart_Backend.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int Product_StatusId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UserAddressId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        //public Address Address { get; set; } // Assuming Address table exists
        public StatusMaster ProductStatus { get; set; }
        public UserAddress UserAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
