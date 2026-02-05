using BankSys.Application.Dtos;
using BankSys.Domain.Filters;

namespace BankSys.Application.Interfaces;

public interface IContaBancariaService
{
    CriarContaBancariaDto CadastrarNovaConta(CriarContaBancariaDto dto);
    IEnumerable<ConsultarContaBancariaDto> ConsultarContasBancarias(ConsultarContasBancariasFilter filter);
    bool InativarConta(string numeroConta);
    void RealizarTransferencia(RealizarTranferenciaBancariaDto dto);
}
