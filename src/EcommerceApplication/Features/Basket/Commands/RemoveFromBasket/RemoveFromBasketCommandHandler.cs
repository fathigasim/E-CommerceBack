
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.DTOs;
using EcommerceDomain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;


namespace EcommerceApplication.Features.Basket.Commands.RemoveFromBasket
{
    public class RemoveFromBasketCommandHandler : IRequestHandler<RemoveFromBasketCommand, Result<BasketDto>>
    {
       // private readonly IBasketRepository _basketRepository;
       // private readonly IProductRepository _productRepository;
        private readonly IBasketContextAccessor _basketContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RemoveFromBasketCommandHandler> _logger;

        public RemoveFromBasketCommandHandler(
           // IBasketRepository basketRepository,
           // IProductRepository productRepository,
            IBasketContextAccessor basketContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<RemoveFromBasketCommandHandler> logger)
        {
         //   _basketRepository = basketRepository;
        //    _productRepository = productRepository;
            _basketContextAccessor = basketContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<BasketDto>> Handle(RemoveFromBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var basketId = _basketContextAccessor.GetBasketId();
                if (string.IsNullOrEmpty(basketId))
                    return Result<BasketDto>.Failure("No basket found");

                var basket = await _unitOfWork.Baskets.GetByIdAsync(basketId, includeItems: true);
                if (basket == null)
                    return Result<BasketDto>.Failure("Basket not found");

                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                var item = basket.BasketItems.FirstOrDefault(i => i.ProductId == request.ProductId);
                if (item == null)
                    return Result<BasketDto>.Failure("Item not found in basket");

                var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
                if (product == null)
                    return Result<BasketDto>.Failure("Product not found");

                if (item.Quantity <= request.Quantity)
                {
                    // Remove entire item
                    product.StockQuantity += item.Quantity;
                    await _unitOfWork.Baskets.RemoveItemAsync(item);
                }
                else
                {
                    // Reduce quantity
                    item.Quantity -= request.Quantity;
                    product.StockQuantity += request.Quantity;
                }

                 _unitOfWork.Products.Update(product);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                // Check if basket is now empty
                var hasItems = basket.BasketItems.Any(i => i.BasketItemId != item.BasketItemId || item.Quantity > 0);
                if (!hasItems)
                {
                    _basketContextAccessor.ClearBasketId();
                }

                basket = await _unitOfWork.Baskets.GetByIdAsync(basketId, includeItems: true);
                var dto = MapToDto(basket);

                return Result<BasketDto>.Success(dto);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error removing item from basket");
                return Result<BasketDto>.Failure("Failed to remove item from basket");
            }
        }

        private BasketDto MapToDto(EcommerceDomain.Entities.Basket basket)
        {
            return new BasketDto(
                basket.BasketId,
                basket.BasketItems.Select(i => new BasketItemDto(
                    i.BasketItemId,
                    i.ProductId,
                    i.Product.Name,
                    i.Quantity,
                    i.Product.Price,
                    i.Product.ImageUrl
                )).ToList(),
                basket.GetTotal(),
                basket.GetItemCount()
            );
        }
    }
}

