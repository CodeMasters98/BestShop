using FluentValidation;

namespace BestShop.ProductService.Application.Dtos.Product;

public record AddProductDto(string name,decimal price,string brandName);

public class AddProductDtoValidator : AbstractValidator<AddProductDto>
{
    public AddProductDtoValidator()
    {
        RuleFor(x => x.name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(5)
            .WithMessage("Please enter valid name. valid name must contain more than 5 characters");
    }
}
