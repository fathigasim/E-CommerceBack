using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.DTOs
{
    public record UpdateProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId
);

}
