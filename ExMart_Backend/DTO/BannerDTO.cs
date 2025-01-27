using System.ComponentModel.DataAnnotations.Schema;

namespace ExMart_Backend.DTO
{
    public class BannerDTO
    {
        public int BannerId { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public string ProductImage { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; } // Ensure this property is present

        public decimal ProductPrice { get; set; }
    }
}