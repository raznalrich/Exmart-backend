using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class HrDetails
    {
        [Key]
        public int Id { get; set; }
        public long PhoneNumber {  get; set; }
        public string HrEmail { get; set; }
        public string HrAddress { get; set;}
        public string HrChatEmail { get; set;}
        public string ProTagLine { get; set; }
    }
}
