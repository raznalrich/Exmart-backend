using System.Text.Json.Serialization;

namespace ExMart_Backend.Model
{
    public class AddressType
    {
        public int Id { get; set; }
        public string AddressTypeName { get; set; }

        [JsonIgnore]
        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}
