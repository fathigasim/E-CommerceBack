using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Basket.DTOs
{
    public record BasketItemDto(
        string Id,
        Guid ProductId,
        string ProductName,
        int Quantity,
        decimal Price,
        string? ImagePath
    )
    {
        public decimal Subtotal => Quantity * Price;
    }
}
