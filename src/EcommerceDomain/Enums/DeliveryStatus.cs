using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Enums
{
    public enum DeliveryStatus
    {
        Pending = 0,        // Order created, not processed yet
        Processing = 1,     // Preparing shipment
        Shipped = 2,        // Out for delivery
        Delivered = 3,      // Successfully delivered
        Failed = 4,         // Delivery attempt failed ❗
        Cancelled = 5       // Order/delivery cancelled
    }
}
