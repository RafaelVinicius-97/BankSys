using BankSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSys.Persistence.Configurations;

public class TranferenciaBancariaConfiguration : IEntityTypeConfiguration<TransferenciaBancaria>
{
    public void Configure(EntityTypeBuilder<TransferenciaBancaria> builder)
    {
        builder.ToTable("TransferenciasBancarias");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.ContaOrigemId)
            .IsRequired();
        builder.Property(c => c.ContaDestinoId)
            .IsRequired();
        builder.Property(c => c.NomeResponsavelTransferencia)
            .IsRequired();
        builder.Property(c => c.ValorTransferencia)
            .HasColumnType("Decimal(18,2)")
            .IsRequired();
        builder.Property(c => c.TransferidoEm)
            .IsRequired();
    }
}
