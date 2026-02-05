namespace BankSys.Domain.Entities;

public class TransferenciaBancaria
{
    public TransferenciaBancaria()
    { }

    public TransferenciaBancaria(Guid contaOrigemId, string nomeResponsavelTransferencia, Guid contaDestinoId, decimal valor)
    {
        if (valor <= 0)
            throw new Exception("Valor da transferência deve ser maior que zero");

        Id = Guid.NewGuid();
        ContaOrigemId = contaOrigemId;
        NomeResponsavelTransferencia = nomeResponsavelTransferencia;
        ContaDestinoId = contaDestinoId;
        ValorTransferencia = valor;
        TransferidoEm = DateTime.UtcNow;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ContaOrigemId { get; private set; }
    public string NomeResponsavelTransferencia { get; set; }
    public Guid ContaDestinoId { get; private set; }
    public decimal ValorTransferencia { get; private set; }
    public DateTime TransferidoEm { get; private set; } = DateTime.UtcNow;
}
