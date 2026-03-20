
using EcommerceApplication.Common.Settings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Commands.RefundPayment
{
    public record RefundPaymentCommand(
     Guid PaymentId,
     decimal? Amount = null   // null = full refund
 ) : IRequest<Result<RefundPaymentResponse>>;
}
