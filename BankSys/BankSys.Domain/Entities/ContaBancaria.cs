using BankSys.Domain.Enums;
using BankSys.Domain.Exceptions;

namespace BankSys.Domain.Entities;

public class ContaBancaria
{
    public ContaBancaria()
    { }

    public ContaBancaria(string nomeTitular, string documentoTitular, decimal saldo)
    {
        NomeTitular = nomeTitular;
        DocumentoTitular = documentoTitular;
        Saldo += (1000M + saldo);
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    public EnumStatusContaBancaria Status { get; private set; } = EnumStatusContaBancaria.Ativa;
    public string NomeTitular { get; private set; }
    public string DocumentoTitular { get; private set; }
    public decimal Saldo { get; private set; } = decimal.Zero;

    public void InativarConta()
    {
        if (Status == EnumStatusContaBancaria.Inativa)
            throw new ContaBancariaException("Conta bancária já está inativa");

        Status = EnumStatusContaBancaria.Inativa;
    }

    public void Debitar(decimal valor)
    {
        ContaValida();

        if (Saldo < valor)
            throw new ContaBancariaException("Saldo Insuficiente");

        Saldo -= valor;
    }

    public void Creditar(decimal valor)
    {
        ContaValida();

        Saldo += valor;
    }

    public void ContaValida()
    {
        if (Status == EnumStatusContaBancaria.Inativa)
            throw new ContaBancariaException($"Conta bancária de documento {DocumentoTitular} está inativa");

        return;
    }
}
