using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Basket.Commands.RemoveFromBasket
{
    public class RemoveFromBasketCommandValidator : AbstractValidator<RemoveFromBasketCommand>
    {
        public RemoveFromBasketCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }

}
