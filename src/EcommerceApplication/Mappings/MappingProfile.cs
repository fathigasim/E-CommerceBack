using AutoMapper;
using EcommerceApplication.DTOs;
using EcommerceApplication.Features.Orders.DTOs;
using EcommerceApplication.Features.Payment.DTOs;
using EcommerceDomain.Entities;
using MediaRTutorialApplication.DTOs;
using MediaRTutorialApplication.Features.Orders.DTOs;
using MediaRTutorialDomain.Entities;

namespace MediaRTutorialApplication.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {

            // Product
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            // Order
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));

            CreateMap<Payment, PaymentDto>();
              
        }
       
    }
}

