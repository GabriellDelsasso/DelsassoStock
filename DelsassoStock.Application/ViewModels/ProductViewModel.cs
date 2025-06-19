using FluentValidation;

namespace DelsassoStock.Application.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; } = String.Empty;
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator() 
        {
            RuleFor(productViewModel => productViewModel.Name)
                .NotEmpty()
                .WithMessage("O Nome é obrigatório");

            RuleFor(productViewModel => productViewModel.Price)
                .NotEmpty()
                .WithMessage("O Preço é obrigatório");
        }
    }
}
