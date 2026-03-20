using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Entities
{
    public class Basket
    {
        public string BasketId { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        public decimal GetTotal() => BasketItems.Sum(i => i.Quantity * i.Product.Price);
        public int GetItemCount() => BasketItems.Count;
    }
}
