
using EcommerceApplication.Common.Settings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Commands.HandleWebhook
{
    public record HandleStripeWebhookCommand(
    string EventType,
    string PaymentIntentId,
    string Status,
    long AmountReceived
) : IRequest<Result<bool>>;
}
