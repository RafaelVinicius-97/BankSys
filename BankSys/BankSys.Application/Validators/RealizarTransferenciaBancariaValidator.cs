using BankSys.Application.Dtos;
using FluentValidation;

namespace BankSys.Application.Validators;

public class RealizarTransferenciaBancariaValidator : AbstractValidator<RealizarTranferenciaBancariaDto>
{
    public RealizarTransferenciaBancariaValidator()
    {
        RuleFor(x => x.NomeCliente)
            .NotEmpty().WithMessage("O nome do responsável pela transferência deve ser informado");
        RuleFor(x => x.DocumentoContaOrigem)
            .NotEmpty().WithMessage("O documento da conta origem deve ser informado");
        RuleFor(x => x.DocumentoContaDestino)
            .NotEmpty().WithMessage("O documento da conta destino deve ser informado");
        RuleFor(x => x.Valor)
            .NotNull().WithMessage("O valor da transferência deve ser informado")
            .GreaterThan(0).WithMessage("O valor para transferência deve ser maior que zero");
    }
}
