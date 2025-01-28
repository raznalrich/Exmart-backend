using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMart_Backend.Model
{
    public class HrDetails
    {
        [Key]
        public int Id { get; set; }
        public long HrPhoneNumber {  get; set; }
        public string HrEmail { get; set; }
        public string HrAddress { get; set;}
        public string HrChatEmail { get; set;}
        public string ProTagLine { get; set; }
    }
}
