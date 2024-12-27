using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
    }
}
