using System.ComponentModel.DataAnnotations;

namespace ExMart_Backend.Model
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressTypeId { get; set; }
        public bool IsPrimary { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public AddressType AddressType { get; set; }
    }
}
