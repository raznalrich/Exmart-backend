using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        //[Required]
        //public string Password { get; set; }
    }
}
