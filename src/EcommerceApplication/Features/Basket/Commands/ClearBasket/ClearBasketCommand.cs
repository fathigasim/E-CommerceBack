using EcommerceApplication.Common.Settings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Basket.Commands.ClearBasket
{
    public record ClearBasketCommand : IRequest<Result<Unit>>;
}
