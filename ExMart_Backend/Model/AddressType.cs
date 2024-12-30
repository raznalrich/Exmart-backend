namespace ExMart_Backend.Model
{
    public class AddressType
    {
        public int Id { get; set; }
        public string AddressTypeName { get; set; }
        public ICollection<UserAddress> UserAddresses { get; set; }
    }
}
