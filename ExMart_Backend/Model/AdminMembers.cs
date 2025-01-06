using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class AdminMembers
    {
        [Key]
        public int Id { get; set; } 

        [ForeignKey("User")]
        public int UserId { get; set; } 

        public DateTime AddedDate { get; set; } = DateTime.UtcNow; 
        public User User { get; set; }
    }
}
