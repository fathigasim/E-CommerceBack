using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Models
{
    public record PaymentIntentResult(
    string PaymentIntentId,
    string ClientSecret,
    string Status,
    decimal Amount,
    string Currency
);
}
