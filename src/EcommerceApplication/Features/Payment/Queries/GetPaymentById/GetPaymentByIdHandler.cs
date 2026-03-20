using AutoMapper;
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Payment.DTOs;
using EcommerceDomain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Queries.GetPaymentById
{
    public class GetPaymentByIdHandler
    : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public GetPaymentByIdHandler(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
                }

        public async Task<Result<PaymentDto>> Handle(
            GetPaymentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository
                .GetByIdAsync(request.PaymentId, cancellationToken);
            var paymentDto= _mapper.Map<PaymentDto>(payment);
            if (payment is null)
                return Result<PaymentDto>.Failure("Payment not found");

            return Result<PaymentDto>.Success(new PaymentDto(
                paymentDto.Id,
                paymentDto.UserId,
                paymentDto.Amount,
                paymentDto.Currency,
                paymentDto.Status.ToString(),
                paymentDto.StripePaymentIntentId,
                paymentDto.OrderId,
                paymentDto.Order,
                payment.CreatedAt));
        }
    }
}
