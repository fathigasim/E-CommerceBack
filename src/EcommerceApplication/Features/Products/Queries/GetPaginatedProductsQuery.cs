using EcommerceApplication.Common;
using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Products.DTOs;
using MediatR;

namespace EcommerceApplication.Features.Products.Queries
{
    
    public record GetPaginatedProductsQuery(string? q,Guid? categoryId,int Page, int PageSize=6)
    : IRequest<Result<PaginatedList<ProductDto>>>;
}
