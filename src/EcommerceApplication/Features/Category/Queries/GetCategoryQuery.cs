

using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using MediatR;


namespace MediaRTutorialApplication.Features.Category.Queries
{
    public record GetCategoryQuery (): IRequest<Result<List<CategoryDto>>>;
    
}
