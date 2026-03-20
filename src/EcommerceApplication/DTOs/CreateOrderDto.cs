using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.DTOs
{
    public record CreateOrderDto(
    List<CreateOrderItemDto> Items
);

}
