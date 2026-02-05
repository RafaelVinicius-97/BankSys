using BankSys.Domain.Entities;
using BankSys.Domain.Filters;

namespace BankSys.Domain.Interfaces;

public interface IContaBancariaRepository
{
    IEnumerable<ContaBancaria> ConsultarContasBancarias(ConsultarContasBancariasFilter filter);
    ContaBancaria? BuscarContaBancaria(string documentoTitular);
    ContaBancaria CriarConta(ContaBancaria contaBancaria);
    bool TitularPossuiContaBancariaComEsseDocumento(string documentoTitular);
}
