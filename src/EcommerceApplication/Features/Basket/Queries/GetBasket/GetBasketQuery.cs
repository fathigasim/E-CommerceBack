
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.DTOs;
using MediatR;


namespace MediaRTutorialApplication.Features.Basket.Queries.GetBasket
{
    public record GetBasketQuery : IRequest<Result<BasketDto>>;
}
