using BankSys.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace BankSys.Persistence.UoW;

public class UoW : IUoW
{
    private readonly BankSysContext _context;
    IDbContextTransaction _transaction;

    public UoW(BankSysContext context)
    {
        _context = context;
    }

    public void SaveChanges() =>
        _context.SaveChanges();

    public void BeginTransaction()
    {
        if (_transaction == null)
            _transaction = _context.Database.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
            return;

        _context.SaveChanges();
        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null!;
    }

    public void RollbackTransaction()
    {
        if (_transaction == null)
            return;

        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null!;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
