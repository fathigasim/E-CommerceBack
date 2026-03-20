using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Models
{
    public record CustomerResult(
    string CustomerId,
    string Email,
    string Name
);
}
