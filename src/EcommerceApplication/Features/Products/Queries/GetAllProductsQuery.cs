using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using MediaRTutorialApplication.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Products.Queries
{
    public record GetAllProductsQuery : IRequest<Result<IReadOnlyList<ProductDto>>>;

}
