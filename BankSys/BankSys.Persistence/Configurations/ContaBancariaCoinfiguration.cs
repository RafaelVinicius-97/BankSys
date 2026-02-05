using BankSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSys.Persistence.Configurations;

public class ContaBancariaCoinfiguration : IEntityTypeConfiguration<ContaBancaria>
{
    public void Configure(EntityTypeBuilder<ContaBancaria> builder)
    {
        builder.ToTable("ContasBancarias");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.CriadoEm)
            .IsRequired();
        builder.Property(c => c.Status)
            .HasColumnType("TinyInt")
            .IsRequired();
        builder.Property(c => c.NomeTitular)
            .IsRequired();
        builder.Property(c => c.DocumentoTitular)
            .IsRequired();
        builder.Property(c => c.Saldo)
            .HasColumnType("Decimal(18,2)")
            .IsRequired();
    }
}
