using System.Text.Json.Serialization;
using ExMart_Backend.Model;

namespace ExMart_Backend.DTO
{
    public class UpdateOrderStatusRequest
    {
        public int OrderItemId { get; set; }
        public int ProductStatusId { get; set; }
        public int? shippingCharge { get; set; }

    }
}