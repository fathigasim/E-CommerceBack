using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.DTOs;
using MediatR;


namespace EcommerceApplication.Features.Basket.Commands.RemoveFromBasket
{
    public record RemoveFromBasketCommand(
        Guid ProductId,
        int Quantity = 1
    ) : IRequest<Result<BasketDto>>;
}
