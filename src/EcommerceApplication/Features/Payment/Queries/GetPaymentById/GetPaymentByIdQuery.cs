using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Payment.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery(Guid PaymentId) : IRequest<Result<PaymentDto>>;

//    public record PaymentDto(
//    Guid Id,
//    string UserId,
//    decimal Amount,
//    string Currency,
//    string Status,
//    string? StripePaymentIntentId,
//    DateTime CreatedAt
//);
}
