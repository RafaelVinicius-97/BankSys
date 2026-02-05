using BankSys.Application.Dtos;
using BankSys.Domain.Entities;

namespace BankSys.Application.Mappers;

public static class ToDto
{
    public static ConsultarContaBancariaDto ConvertToConsultarContaBancariaDto(this ContaBancaria entity) =>
        new()
        {
            NomeTitular = entity.NomeTitular,
            DocumentoTitular = entity.DocumentoTitular,
            Saldo = entity.Saldo,
            CriadoEm = entity.CriadoEm,
            Status = entity.Status
        };

    public static CriarContaBancariaDto ConvertToCriarContaBancariaDto(this ContaBancaria entity) =>
        new()
        {
            NomeTitular = entity.NomeTitular,
            DocumentoTitular = entity.DocumentoTitular,
            Saldo = entity.Saldo
        };

    public static IEnumerable<ConsultarContaBancariaDto> ConvertToDto(this IEnumerable<ContaBancaria> entities) =>
        entities.Select(e => e.ConvertToConsultarContaBancariaDto());
}
