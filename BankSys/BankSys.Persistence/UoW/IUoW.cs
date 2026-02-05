namespace BankSys.Persistence.UoW;

public interface IUoW
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
    void SaveChanges();
}
