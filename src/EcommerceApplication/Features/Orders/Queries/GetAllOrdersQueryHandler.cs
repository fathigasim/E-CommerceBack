using AutoMapper;
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Orders.DTOs;
using EcommerceDomain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Orders.Queries
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<IReadOnlyList<OrderDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrdersAsync(cancellationToken);
            if (orders.Any())
            {
                var ordersDto = _mapper.Map<List<OrderDto>>(orders);
             
                
                    return Result<IReadOnlyList<OrderDto>>.Success(ordersDto);
            }
            return Result<IReadOnlyList<OrderDto>>.Failure("No orders to fetch");
        }
    }
}
