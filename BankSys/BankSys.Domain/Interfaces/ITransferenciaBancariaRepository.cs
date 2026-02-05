using BankSys.Domain.Entities;

namespace BankSys.Domain.Interfaces;

public interface ITransferenciaBancariaRepository
{
    Task AdicionarTransferencia(TransferenciaBancaria transferencia);
}
