using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExMart_Backend.Model
{
    public class StatusMaster
    {
        [Key]
        public int Product_StatusId { get; set; }
        public string StatusName { get; set; }

        // Navigation Properties
        public ICollection<Order> Orders { get; set; }

        [JsonIgnore]

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
