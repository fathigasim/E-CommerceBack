using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Features.Auth.Commands.UserManagement.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
           
            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(6).WithMessage("Password must be at least 6 characters")
               .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
               .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
               .Matches(@"[0-9]").WithMessage("Password must contain at least one number")
               .Matches(@"[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.NewPasswordConfirm).NotEmpty().WithMessage("New password confirm is required")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }
}
