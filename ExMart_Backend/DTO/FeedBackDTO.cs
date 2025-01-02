using System.ComponentModel.DataAnnotations.Schema;

namespace ExMart_Backend.DTO
{
    public class FeedBackDTO
    {
        public int FeedBackId { get; set; }
        
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string FeedBack { get; set; }
    }
}
