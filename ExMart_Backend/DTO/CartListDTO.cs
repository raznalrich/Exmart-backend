using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.DTO
{
    public class CartListDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column(Order = 1, TypeName = "Serial")]
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int SizeId { get; set; }
        public decimal Price {  get; set; }
        public string SizeName { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
