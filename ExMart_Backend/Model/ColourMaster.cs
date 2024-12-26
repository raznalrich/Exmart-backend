using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class ColourMaster
    {
        [Key]
        public int ColorId { get; set; }
        public string ColorName { get; set; }    
        public string ColorCode { get; set; }    
    }
}
