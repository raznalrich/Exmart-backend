using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMart_Backend.Model
{
    public class AddToCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column(Order = 1, TypeName = "Serial")]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
