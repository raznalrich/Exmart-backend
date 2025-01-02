namespace ExMart_Backend.DTO
{
    public class OrderDetailByOrderDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int OrderId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal TotalAmount { get; set; }

        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
