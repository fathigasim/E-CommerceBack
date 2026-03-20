using EcommerceApplication.Common;
using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using MediatR;

namespace EcommerceApplication.Features.Products.Queries
{
    
    public record GetPaginatedProductsQuery(int Page, int PageSize)
    : IRequest<Result<PaginatedList<ProductDto>>>;
}
