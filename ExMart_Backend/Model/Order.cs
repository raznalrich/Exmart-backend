using System.Net;
using System.Text.Json.Serialization;

namespace ExMart_Backend.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
       
        public DateTime? CreatedAt { get; set; }
        public int? UserAddressId { get; set; }

        // Navigation Properties
        [JsonIgnore]

        public User User { get; set; }
        //public Address Address { get; set; } // Assuming Address table exists
       
        public UserAddress UserAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
