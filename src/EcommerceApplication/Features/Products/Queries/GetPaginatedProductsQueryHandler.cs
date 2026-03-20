using AutoMapper;
using EcommerceApplication.Common;
using EcommerceApplication.Common.Settings;
using EcommerceApplication.DTOs;
using EcommerceApplication.Features.Payment.DTOs;
using EcommerceApplication.Features.Products.Queries;
using EcommerceDomain.Enums;
using EcommerceDomain.Interfaces;
using MediatR;


namespace EcommerceApplication.Features.Products.Queries
{
    public class GetPaginatedProductsQueryHandler :IRequestHandler<GetPaginatedProductsQuery,Result<PaginatedList<ProductDto>>>
    {                                           

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetPaginatedProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }


        public async Task<Result<PaginatedList<ProductDto>>> Handle(
    GetPaginatedProductsQuery request,
    CancellationToken cancellationToken)
        {
            // var (items, total) =
            var result = await _unitOfWork.Products.GetPagedAsync<ProductDto>(
          request.Page,
          request.PageSize,
        //  filter: p => p. == PaymentStatus.Succeeded,
          orderBy: q => q.OrderByDescending(p => p.CreatedAt),
          cancellationToken: cancellationToken
      );
            //items.Count();
          //var mappedDto=  _mapper.Map<List<ProductDto>>(items);

           
            //var dtoItems = mappedDto
            //    .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity, p.CategoryId,p.ca.CategoryName))
            //    .ToList();

            return Result<PaginatedList<ProductDto>>.Success(
                new PaginatedList<ProductDto> {
                  Items=  result.Items,
                  PageNumber=  request.Page,
                  PageSize=  request.PageSize,
                 TotalCount= result.TotalCount});
        }
    }
}