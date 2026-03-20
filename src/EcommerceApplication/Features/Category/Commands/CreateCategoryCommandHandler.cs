
using EcommerceApplication.Common.Settings;
using EcommerceDomain.Interfaces;
using MediatR;


namespace MediaRTutorialApplication.Features.Category.Commands
{
    public class CreateCategoryCommandHandler : MediatR.IRequestHandler<CreateCategoryCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new EcommerceDomain.Entities.Category
            {
                Name = request.Name,
                Description = request.Description
            };
           await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
