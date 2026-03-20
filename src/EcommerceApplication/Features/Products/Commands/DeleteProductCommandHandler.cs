using EcommerceApplication.Common.Settings;
using EcommerceDomain.Interfaces;
using MediatR;

namespace MediaRTutorialApplication.Features.Products.Commands
{
    public class DeleteProductCommandHandler
     : IRequestHandler<DeleteProductCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(
            DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Products
                .GetByIdAsync(request.Id, cancellationToken);

            if (product is null)
                return Result<Unit>.Failure("Product not found.");

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Unit>.Success(Unit.Value);
        }
    }

}
