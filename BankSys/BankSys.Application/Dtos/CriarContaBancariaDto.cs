namespace BankSys.Application.Dtos;

public record CriarContaBancariaDto(
    string NomeTitular,
    string DocumentoTitular,
    decimal? Saldo)
{
    public CriarContaBancariaDto() : this(string.Empty, string.Empty, null)
    {
    }
}