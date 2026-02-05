using BankSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankSys.Persistence.Context;

public class BankSysContext : DbContext
{
    public BankSysContext(DbContextOptions<BankSysContext> options) 
        : base(options)
    { }

    public DbSet<ContaBancaria> ContasBancarias { get; set; }
    public DbSet<TransferenciaBancaria> TranferenciasBancarias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankSysContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
