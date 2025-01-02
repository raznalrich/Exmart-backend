namespace ExMart_Backend.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public bool IsPrimary { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string AddressTypeName { get; set; }
    }

    public class AddAddressDTO
    {
        public int UserId { get; set; }
        public int AddressTypeId { get; set; }
        public bool IsPrimary { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

}
