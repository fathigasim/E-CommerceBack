
using EcommerceApplication.Common.Settings;
using EcommerceDomain.Entities;
using EcommerceDomain.Enums;
using EcommerceDomain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Commands.CreatePaymentIntent
{
    public class CreatePaymentIntentHandler
     : IRequestHandler<CreatePaymentIntentCommand, Result<CreatePaymentIntentResponse>>
    {
        private readonly IPaymentService _paymentService;
       private readonly IBasketContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePaymentIntentHandler> _logger;

        public CreatePaymentIntentHandler(
            IPaymentService paymentService,
            IBasketContextAccessor contextAccessor,
            IUnitOfWork unitOfWork,
            ILogger<CreatePaymentIntentHandler> logger)
        {
            _paymentService = paymentService;
            _contextAccessor = contextAccessor;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<CreatePaymentIntentResponse>> Handle(
            CreatePaymentIntentCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var basket = await _unitOfWork.Baskets.GetByIdAsync(_contextAccessor.GetBasketId(), includeItems: true);
                var basketItems = basket?.BasketItems ?? new List<BasketItem>();
                // 1. Call Stripe via our abstraction
            //    var metadata = new Dictionary<string, string>
            //{
            //    { "userId", request.UserId },
            //        {"productName",string.Join(" ,",basketItems.Select(p=>p.Product.Name)) }

            //};
                var metadata = new Dictionary<string, string>
        {
            { "userId", request.UserId },
            //{ "basketId", basket.BasketId.ToString() },
            { "productNames", string.Join(", ", basketItems.Select(p => p.Product.Name)) },
            { "productIds", string.Join(", ", basketItems.Select(p => p.ProductId.ToString())) },
            { "itemCount", basketItems.Count.ToString() }
        };
                var intentResult = await _paymentService.CreatePaymentIntentAsync(
                    request.Amount,
                    request.Currency,
                    request.CustomerEmail,
                    metadata,
                    cancellationToken);

                // 2. Persist payment record in our database
                var payment = new EcommerceDomain.Entities.Payment
                {
                 //   Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    StripePaymentIntentId = intentResult.PaymentIntentId,
                    Status = PaymentStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                };

                await  _unitOfWork.Payments.AddAsync(payment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                // 3. Return client secret to frontend
                return Result<CreatePaymentIntentResponse>.Success(
                    new CreatePaymentIntentResponse(
                        payment.Id,
                        intentResult.ClientSecret,
                        intentResult.PaymentIntentId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create payment intent");
                return Result<CreatePaymentIntentResponse>.Failure(ex.Message);
            }
        }
    }
}
