using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Model
{
    public class Feedback
    {
        public int FeedBackId { get; set; }
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public string FeedBack { get; set; }

        [JsonIgnore]
        public User User { get; set; }

    }
}
