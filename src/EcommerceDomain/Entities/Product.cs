
using EcommerceDomain.Common;
using MediaRTutorialDomain.Entities;

namespace EcommerceDomain.Entities
{
    public class Product : BaseEntity, IAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public Guid CategoryId { get; set; }
        public string? ImageUrl { get; set; }       // ← Add this
        public string? ImageFileName { get; set; }   // ← Add this
        public Category Category { get; set; } = null!;

        public ICollection<OrderItem>? OrderItems { get; set; } 
    }


}
