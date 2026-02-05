using BankSys.Application.Dtos;
using FluentValidation;

namespace BankSys.Application.Validators;

public class CriarContaBancariaValidator : AbstractValidator<CriarContaBancariaDto>
{
    public CriarContaBancariaValidator()
    {
        RuleFor(x => x.NomeTitular)
            .NotEmpty().WithMessage("O nome do titular é obrigatório.");
        RuleFor(x => x.DocumentoTitular)
            .NotEmpty().WithMessage("O documento do titular é obrigatório.");
        RuleFor(x => x.Saldo)
            .GreaterThan(0).WithMessage("O saldo deve ser maior que zero.");
    }
}
