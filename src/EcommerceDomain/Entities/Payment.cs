using EcommerceDomain.Enums;

using EcommerceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceDomain.Common;
using MediaRTutorialDomain.Entities;

namespace EcommerceDomain.Entities
{
    public class Payment : BaseEntity, IAuditableEntity
    {
     
        public string UserId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string? StripePaymentIntentId { get; set; }
        public string? StripeCustomerId { get; set; }
        public PaymentStatus Status { get; set; }
        public Guid? OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
