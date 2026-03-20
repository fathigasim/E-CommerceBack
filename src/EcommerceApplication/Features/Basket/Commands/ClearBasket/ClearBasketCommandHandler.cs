using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.Commands.ClearBasket;
using EcommerceDomain.Interfaces;
using MediatR;


namespace MediaRTutorialApplication.Features.Basket.Commands.ClearBasket
{
    public class ClearBasketCommandHandler : IRequestHandler<ClearBasketCommand, Result<Unit>>
    {
       // private readonly IBasketRepository _basketRepository;
       // private readonly IProductRepository _productRepository;
        private readonly IBasketContextAccessor _basketContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ClearBasketCommandHandler(
           // IBasketRepository basketRepository,
           // IProductRepository productRepository,
            IBasketContextAccessor basketContextAccessor,
            IUnitOfWork unitOfWork)
        {
           // _basketRepository = basketRepository;
           // _productRepository = productRepository;
            _basketContextAccessor = basketContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var basketId = _basketContextAccessor.GetBasketId();
                if (string.IsNullOrEmpty(basketId))
                    return Result<Unit>.Success(Unit.Value);

                var basket = await _unitOfWork.Baskets.GetByIdAsync(basketId, includeItems: true);
                if (basket == null || !basket.BasketItems.Any())
                    return Result<Unit>.Success(Unit.Value);

                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                // Restore stock for all items
                var productIds = basket.BasketItems.Select(i => i.ProductId).Distinct();
                var products = await _unitOfWork.Products.GetProductsByIdAsync(productIds);

                foreach (var product in products)
                {
                    var totalQuantity = basket.BasketItems
                        .Where(i => i.ProductId == product.Id)
                        .Sum(i => i.Quantity);
                    product.StockQuantity += totalQuantity;
                    // _unitOfWork.Products.Update(product);
                }

                // Remove all items
                foreach (var item in basket.BasketItems.ToList())
                {
                    await _unitOfWork.Baskets.RemoveItemAsync(item);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                _basketContextAccessor.ClearBasketId();

                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                return Result<Unit>.Failure("Failed to clear basket");
            }
        }
    }
}
