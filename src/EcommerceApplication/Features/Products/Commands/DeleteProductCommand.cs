using EcommerceApplication.Common.Settings;
using MediatR;


namespace MediaRTutorialApplication.Features.Products.Commands
{

    public record DeleteProductCommand(Guid Id) : IRequest<Result<Unit>>;


}
