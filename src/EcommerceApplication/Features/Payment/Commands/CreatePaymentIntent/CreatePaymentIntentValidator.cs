using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Payment.Commands.CreatePaymentIntent
{
    public class CreatePaymentIntentValidator : AbstractValidator<CreatePaymentIntentCommand>
    {
        public CreatePaymentIntentValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero")
                .LessThanOrEqualTo(999999.99m).WithMessage("Amount exceeds maximum");

            RuleFor(x => x.Currency)
                .NotEmpty()
                .Length(3).WithMessage("Currency must be a 3-letter ISO code");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().EmailAddress();
        }
    }
}
