

namespace MediaRTutorialApplication.Features.Orders.DTOs
{
    public record OrderItemDto(
    Guid ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice
);

}
