namespace ExMart_Backend.DTO
{
    public class OrderListDTO
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItems { get; set; }
    }
}
