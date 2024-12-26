using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class SizeMaster
    {
        [Key]

        public int SizeId { get; set; }
        public string Size { get; set; }
      
    }
}
