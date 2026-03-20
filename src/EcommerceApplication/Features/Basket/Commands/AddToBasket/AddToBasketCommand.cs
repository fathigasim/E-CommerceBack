using EcommerceApplication.Common.Settings;
using EcommerceApplication.Features.Basket.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Basket.Commands.AddToBasket
{
    public class AddToBasketCommand : IRequest<Result<BasketDto>>
    {

     public Guid ProductId { get; set; }
   
    public int Quantity { get; set; }
  
    } 

  

}
