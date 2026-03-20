
using EcommerceApplication.Common.Settings;
using MediaRTutorialApplication.Features.Basket.DTOs;
using MediatR;


namespace MediaRTutorialApplication.Features.Basket.Queries.GetBasketSummary
{
    public record GetBasketSummaryQuery : IRequest<Result<BasketSummaryDto>>;
}
