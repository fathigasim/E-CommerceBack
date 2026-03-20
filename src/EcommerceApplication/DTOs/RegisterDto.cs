using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.DTOs
{
    public record RegisterDto(string FirstName,string LastName,string UserName,string Email,string Password,bool EmailConfirmed);
    
}
