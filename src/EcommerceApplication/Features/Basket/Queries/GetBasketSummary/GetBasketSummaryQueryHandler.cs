
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.DTOs;
using EcommerceDomain.Interfaces;
using MediaRTutorialApplication.Features.Basket.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Basket.Queries.GetBasketSummary
{
    public class GetBasketSummaryQueryHandler : IRequestHandler<GetBasketSummaryQuery, Result<BasketSummaryDto>>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketContextAccessor _basketContextAccessor;

        public GetBasketSummaryQueryHandler(
            IBasketRepository basketRepository,
            IBasketContextAccessor basketContextAccessor)
        {
            _basketRepository = basketRepository;
            _basketContextAccessor = basketContextAccessor;
        }

        public async Task<Result<BasketSummaryDto>> Handle(GetBasketSummaryQuery request, CancellationToken cancellationToken)
        {
            var basketId = _basketContextAccessor.GetBasketId();
            if (string.IsNullOrEmpty(basketId))
                return Result<BasketSummaryDto>.Success(new BasketSummaryDto(0, 0m));

            var basket = await _basketRepository.GetByIdAsync(basketId, includeItems: true);
            if (basket == null)
                return Result<BasketSummaryDto>.Success(new BasketSummaryDto(0, 0m));

            var summary = new BasketSummaryDto(basket.GetItemCount(), basket.GetTotal());
            return Result<BasketSummaryDto>.Success(summary);
        }
    }
}
