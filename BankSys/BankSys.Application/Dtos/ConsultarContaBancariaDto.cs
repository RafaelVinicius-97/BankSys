using BankSys.Domain.Enums;
using System;

namespace BankSys.Application.Dtos;

public record ConsultarContaBancariaDto(
    string NomeTitular,
    string DocumentoTitular,
    decimal Saldo,
    DateTime CriadoEm,
    EnumStatusContaBancaria Status
)
{
    public ConsultarContaBancariaDto()
        : this(string.Empty, string.Empty, 0m, default, default)
    {
    }
}