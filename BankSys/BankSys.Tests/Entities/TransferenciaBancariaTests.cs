using BankSys.Domain.Entities;
using FluentAssertions;

namespace BankSys.Tests.Entities;

public class TransferenciaBancariaTests
{
    [Fact]
    public void Construtor_DeveCriarTransferenciaValida()
    {
        // Arrange
        var contaOrigemId = Guid.NewGuid();
        var contaDestinoId = Guid.NewGuid();
        var nomeResponsavel = "Rafael";
        var valor = 200m;

        // Act
        var transferencia = new TransferenciaBancaria(
            contaOrigemId,
            nomeResponsavel,
            contaDestinoId,
            valor
        );

        // Assert
        transferencia.Id.Should().NotBeEmpty();
        transferencia.ContaOrigemId.Should().Be(contaOrigemId);
        transferencia.ContaDestinoId.Should().Be(contaDestinoId);
        transferencia.NomeResponsavelTransferencia.Should().Be(nomeResponsavel);
        transferencia.ValorTransferencia.Should().Be(valor);
        transferencia.TransferidoEm.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(2));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Construtor_ValorMenorOuIgualZero_DeveLancarExcecao(decimal valor)
    {
        // Act
        Action act = () =>
            new TransferenciaBancaria(
                Guid.NewGuid(),
                "Rafael",
                Guid.NewGuid(),
                valor
            );

        // Assert
        act.Should().Throw<Exception>()
           .WithMessage("Valor da transferência deve ser maior que zero");
    }
}
