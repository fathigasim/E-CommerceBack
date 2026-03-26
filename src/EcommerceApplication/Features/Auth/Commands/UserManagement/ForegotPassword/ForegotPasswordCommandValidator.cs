using EcommerceApplication.Features.Auth.Commands.UserManagement.ResetPassword;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Auth.Commands.UserManagement.ForegotPassword
{
    public class ForegotPasswordCommandValidator : AbstractValidator<ForegotPasswordCommand>
    {
        public ForegotPasswordCommandValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}
