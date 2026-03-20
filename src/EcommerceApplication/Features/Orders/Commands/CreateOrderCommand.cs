using EcommerceApplication.Common.Settings;

using MediatR;


namespace EcommerceApplication.Features.Orders.Commands
{
    

    // CreateOrderCommand.cs
    public class CreateOrderCommand : IRequest<Result<Unit>>
    {
        public string? PaymentIntentId { get; set; }
    }
}
