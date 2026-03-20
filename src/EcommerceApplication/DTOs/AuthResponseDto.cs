
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.DTOs
{
    public record AuthResponseDto(string Id,
                 string Email,
              string token,
               DateTime theDate);
}
   
