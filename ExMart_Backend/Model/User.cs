using System.ComponentModel.DataAnnotations.Schema;

namespace ExMart_Backend.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedAt { get; set; }



        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    }
}
