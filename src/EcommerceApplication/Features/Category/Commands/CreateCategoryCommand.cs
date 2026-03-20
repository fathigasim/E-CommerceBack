
using EcommerceApplication.Common.Settings;
using MediatR;


namespace MediaRTutorialApplication.Features.Category.Commands
{
    public record CreateCategoryCommand(string Name,string Description) : IRequest<Result<Unit>>;
    
}
