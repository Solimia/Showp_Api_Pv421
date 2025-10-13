using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using FluentValidation;

public class EditProductDtoValidator : AbstractValidator<EditProductDto>
{
    public EditProductDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Product Id must be greater than 0.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .Matches("^[A-Z].*").WithMessage("{PropertyName} must start with an uppercase letter.");

        RuleFor(x => x.Discount)
            .InclusiveBetween(0, 100)
            .WithMessage("Discount must be between 0 and 100.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be non-negative.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity must be non-negative.");

        RuleFor(x => x.CategoryId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("CategoryId must be greater than 0.");

        RuleFor(x => x.Description)
            .MinimumLength(10)
            .MaximumLength(3000)
            .WithMessage("Description must be between 10 and 3000 characters.");
    }
}
