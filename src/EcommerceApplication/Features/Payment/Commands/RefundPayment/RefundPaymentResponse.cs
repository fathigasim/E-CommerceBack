using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Commands.RefundPayment
{
    public record RefundPaymentResponse(string RefundId, string Status, decimal AmountRefunded);
}
