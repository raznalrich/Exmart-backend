namespace ExMart_Backend.Model
{
    public class UserAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressTypeId { get; set; }
        public bool IsPrimary { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
