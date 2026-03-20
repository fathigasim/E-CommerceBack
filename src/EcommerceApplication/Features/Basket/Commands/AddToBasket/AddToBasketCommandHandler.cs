using AutoMapper;
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.Commands.AddToBasket;
using EcommerceApplication.Features.Basket.DTOs;
using EcommerceDomain.Entities;
using EcommerceDomain.Interfaces;
using MediaRTutorialApplication.Features.Basket.DTOs;

using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Basket.Commands.AddToBasket
{
    public class AddToBasketCommandHandler : IRequestHandler<AddToBasketCommand, Result<BasketDto>>
    {
      //  private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBasketContextAccessor _basketContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddToBasketCommandHandler> _logger;
        private readonly IMapper mapper;

        public AddToBasketCommandHandler(
           // IBasketRepository basketRepository,
            IProductRepository productRepository,
            IBasketContextAccessor basketContextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<AddToBasketCommandHandler> logger)
        {
          //  _basketRepository = basketRepository;
            _productRepository = productRepository;
            _basketContextAccessor = basketContextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<BasketDto>> Handle(AddToBasketCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                // Get or create basket
                var basketId = _basketContextAccessor.GetBasketId();
                EcommerceDomain.Entities.Basket basket;

                if (string.IsNullOrEmpty(basketId))
                {
                    basket = new EcommerceDomain.Entities.Basket();
                    await _unitOfWork.Baskets.CreateAsync(basket);
                    _basketContextAccessor.SetBasketId(basket.BasketId);
                }
                else
                {
                    basket = await _unitOfWork.Baskets.GetByIdAsync(basketId, includeItems: true);
                    if (basket == null)
                    {
                        basket = new EcommerceDomain.Entities.Basket();
                        await _unitOfWork.Baskets.CreateAsync(basket);
                        _basketContextAccessor.SetBasketId(basket.BasketId);
                    }
                }

                // Get product and check stock
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                    return Result<BasketDto>.Failure("Product not found");

                if (product.StockQuantity < request.Quantity)
                    return Result<BasketDto>.Failure($"Insufficient stock. Available: {product.StockQuantity}");

                // Check if item already exists
                var existingItem = await _unitOfWork.Baskets.GetBasketItemAsync(basket.BasketId, request.ProductId);

                if (existingItem != null)
                {
                    // Check total quantity
                    var totalQuantity = existingItem.Quantity + request.Quantity;
                    if (product.StockQuantity < totalQuantity)
                        return Result<BasketDto>.Failure($"Insufficient stock. Available: {product.StockQuantity}");

                    existingItem.Quantity += request.Quantity;
                }
                else
                {
                    var newItem = new BasketItem
                    {
                        BasketId = basket.BasketId,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity
                    };
                    await _unitOfWork.Baskets.AddItemAsync(newItem);
                }

                // Update product stock
                product.StockQuantity -= request.Quantity;
                 _productRepository.Update(product);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                _logger.LogInformation("Added {Quantity}x {ProductId} to basket {BasketId}",
                    request.Quantity, request.ProductId, basket.BasketId);

                // Reload basket with items for DTO mapping
                basket = await _unitOfWork.Baskets.GetByIdAsync(basket.BasketId, includeItems: true);
                var dto = MapToDto(basket);

                return Result<BasketDto>.Success(dto);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(ex, "Error adding item to basket");
                return Result<BasketDto>.Failure("Failed to add item to basket");
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

