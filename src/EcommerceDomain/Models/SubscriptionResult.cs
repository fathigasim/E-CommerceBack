using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Models
{
    public record SubscriptionResult(
    string SubscriptionId,
    string Status,
    string CustomerId,
    string PriceId,
    DateTime CurrentPeriodEnd
);
}
