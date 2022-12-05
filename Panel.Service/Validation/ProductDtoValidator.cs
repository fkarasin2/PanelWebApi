using FluentValidation;
using Panel.DTOs;

namespace Panel.Service.Validation;

public class ProductDtoValidator: AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x=>x.name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(x=>x.price).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater than 0");
        RuleFor(x=>x.stock).InclusiveBetween(0,int.MaxValue).WithMessage("{PropertyName} must be greater than or equal to 0");
        RuleFor(x=>x.CategoryId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
    }   
}