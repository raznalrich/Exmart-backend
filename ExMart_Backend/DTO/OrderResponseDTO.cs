namespace ExMart_Backend.DTO
{
    public class OrderResponseDTO
    {
       
        
            public int OrderId { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }


            public DateTime CreatedAt { get; set; }
            public List<OrderItemResponseDTO> OrderItems { get; set; }
        
    }
}
