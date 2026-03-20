using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaRTutorialApplication.Features.Category.Commands
{
    public class CreateCategoryCommandValidator :AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
           
        }
    }
}
