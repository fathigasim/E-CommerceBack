using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using EcommerceApplication.Features.Products.DTOs;
using EcommerceApplication.Features.Products.Queries;
using EcommerceApplication.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.ReqResService.Queries
{
    public class ReqResUserQueryHandler :IRequestHandler<ReqResUserQuery, Result<ReqResUserDto>>
    {
        private readonly IReqResService _reqResService;
        public ReqResUserQueryHandler(IReqResService reqResService) { 
           _reqResService = reqResService;
        }
             public async Task<Result<ReqResUserDto>> Handle(ReqResUserQuery request, CancellationToken cancellationToken)
        {
            var userInfo= await   _reqResService.GetUserByIdAsync(request.id,cancellationToken);
            if (userInfo != null)
            {
                return Result<ReqResUserDto>.Success(userInfo);
            }
            return Result<ReqResUserDto>.Failure("Not able to fetch data");
        }
    }
}
