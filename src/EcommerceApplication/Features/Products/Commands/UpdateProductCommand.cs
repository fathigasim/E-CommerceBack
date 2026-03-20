using EcommerceApplication.Common.Settings;
using MediatR;

namespace MediaRTutorialApplication.Features.Products.Commands
{
    //    public record UpdateProductCommand(
    //    int Id,
    //    string Name,
    //    decimal Price
    //) : IRequest<Unit>, ICacheInvalidatorCommand
    //    {
    //        // When this command runs, clear the specific product cache
    //        public string[] CacheKeys => new[] { $"product-{Id}", "all-products" };
    //    }
    public record UpdateProductCommand(
      Guid Id,
      string Name,
      string Description,
      decimal Price,
      int StockQuantity,
      ImageUploadData? Image,
      Guid CategoryId
  ) : IRequest<Result<Unit>>;

    // A clean DTO to decouple from IFormFile (which is an API concern)
    public record ImageUploadUpdateData(
        Stream Stream,
        string FileName,
        string ContentType
    );

}
