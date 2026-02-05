using BankSys.Domain.Entities;
using BankSys.Domain.Interfaces;
using BankSys.Persistence.Context;

namespace BankSys.Persistence.Repositories;

public class TransferenciaBancariaRepository : ITransferenciaBancariaRepository
{
    private readonly BankSysContext _context;
    public TransferenciaBancariaRepository(BankSysContext context)
    {
        _context = context;
    }

    public async Task AdicionarTransferencia(TransferenciaBancaria transferencia) =>
        await _context.TranferenciasBancarias.AddAsync(transferencia);
}
