using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Basket.DTOs
{
    public record BasketDto(
       string BasketId,
       List<BasketItemDto> Items,
       decimal Total,
       int ItemCount
   )
    {
        public static BasketDto Empty => new(string.Empty, new List<BasketItemDto>(), 0m, 0);
    }
}
