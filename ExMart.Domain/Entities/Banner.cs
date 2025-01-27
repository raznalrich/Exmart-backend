using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExMart_Backend.Model
{
    public class Banner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BannerId { get; set; }
        public string ImageUrl { get; set; }
        public string ProductName { get; set; }

        [ForeignKey("ProductId")]

        public int ProductId { get; set; }




    }
}