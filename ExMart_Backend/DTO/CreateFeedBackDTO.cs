using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ExMart_Backend.DTO
{
    public class CreateFeedBackDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [JsonPropertyName("feedback")]  // Match the property name in your error
        public string FeedBack { get; set; }
    }
}
