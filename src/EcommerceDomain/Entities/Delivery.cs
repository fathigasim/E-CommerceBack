using EcommerceDomain.Enums;
using MediaRTutorialDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomain.Entities
{
    public class Delivery : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public DateTime? ShippedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public int AttemptCount { get; set; }
        public string? FailureReason { get; set; }
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
    }
}
