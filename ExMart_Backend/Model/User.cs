using System.ComponentModel.DataAnnotations.Schema;

namespace ExMart_Backend.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }



        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    }
}
