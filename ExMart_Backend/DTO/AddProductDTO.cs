using ExMart_Backend.Model;

namespace ExMart_Backend.DTO
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public List<int> Size { get; set; }
        public List<int> Color { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }

    }
}
