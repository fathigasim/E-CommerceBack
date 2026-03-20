using EcommerceApplication.Common.Settings;
using MediatR;


namespace MediaRTutorialApplication.Features.Payment.Commands.CreatePaymentIntent
{
    public record CreatePaymentIntentCommand(
    string UserId,
    decimal Amount,
    string Currency,
    string CustomerEmail
) : IRequest<Result<CreatePaymentIntentResponse>>;
}
