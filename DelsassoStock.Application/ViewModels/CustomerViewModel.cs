using DelsassoStock.Domain.ValueObjects;
using FluentValidation;

namespace DelsassoStock.Application.ViewModels
{
    public class CustomerViewModel
    {
        public string Name { get; set; } = String.Empty;
        public string Cpf { get; set; } = String.Empty;
    }

    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(customerViewModel => customerViewModel.Name)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres")
                .MaximumLength(100).WithMessage("Nome não pode exceder 100 caracteres");

            RuleFor(customerViewModel => customerViewModel.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório")
                .Must(BeAValidCpf).WithMessage("CPF inválido")
                .Length(11, 14).WithMessage("CPF deve ter 11 dígitos ou 14 caracteres formatado");
        }

        private bool BeAValidCpf(string cpf)
        {
            try 
            {
                _ = new Cpf(cpf);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
