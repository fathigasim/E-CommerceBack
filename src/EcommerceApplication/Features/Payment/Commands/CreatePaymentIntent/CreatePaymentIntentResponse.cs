using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Commands.CreatePaymentIntent
{
    public record CreatePaymentIntentResponse(
    Guid PaymentId,
    string ClientSecret,
    string PaymentIntentId
);
}
