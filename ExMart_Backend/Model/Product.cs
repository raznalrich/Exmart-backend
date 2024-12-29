using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column(Order = 1, TypeName = "Serial")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public List<string> Size { get; set; }
        public List<string> Color { get; set; }
        public string? PrimaryImageUrl { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int CreatedBy { get; set; }

        // Navigation Properties
        public ICollection<OrderItem> OrderItems { get; set; }
        public bool IsActive {  get; set; }

        public virtual ICollection<ProductImages> ProductImages { get; set; }
    }
}