using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class StatusMaster
    {
        [Key]
        public int Product_StatusId { get; set; }
        public string StatusName { get; set; }

        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    }
}
