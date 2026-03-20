using EcommerceApplication.Common.Settings;
using EcommerceDomain.Interfaces;
using MediaRTutorialApplication.Interfaces;
using MediatR;


namespace MediaRTutorialApplication.Features.Products.Commands
{
    public class UploadProductImageHandler : IRequestHandler<UploadProductImageCommand, Result<string>>
    {
        private readonly IFileStorageService _fileStorage;
        private readonly IUnitOfWork _unitOfWork;

        public UploadProductImageHandler(
            IFileStorageService fileStorage,
            IUnitOfWork unitOfWork)
        {
            _fileStorage = fileStorage;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(
            UploadProductImageCommand request,
            CancellationToken ct)
        {

            // Validation
            if (request.FileContent.Length > 5_000_000) // 5MB
                return Result<string>.Failure("File too large");

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(request.ContentType))
                return Result<string>.Failure("Invalid file type");

            // Generate unique filename
            var uniqueFileName = $"{Guid.NewGuid()}_{request.FileName}";

            // Save file via service
            var fileUrl = await _fileStorage.UploadAsync(
                request.FileContent,
                uniqueFileName,
                request.ContentType);

            // Update database
            //var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            //if (product == null)
            //    return Result<string>.Failure("Product not found");

           // product.ImageUrl = fileUrl;
            await _unitOfWork.SaveChangesAsync();

            return Result<string>.Success(fileUrl);
        }
    }
}
