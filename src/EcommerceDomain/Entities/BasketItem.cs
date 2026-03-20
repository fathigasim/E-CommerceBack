using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Entities
{
    public class BasketItem 
    {
        public string BasketItemId { get; set; } = Guid.NewGuid().ToString();
        public string BasketId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Basket Basket { get; set; }
        public Product Product { get; set; }
    }
}
