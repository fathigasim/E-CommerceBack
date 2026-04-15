using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.ReqResService.Queries
{
    public record ReqResUserQuery(int id) :IRequest<Result<ReqResUserDto>>;
   
}
