using BankSys.Domain.Entities;
using BankSys.Domain.Enums;
using BankSys.Domain.Exceptions;
using FluentAssertions;

namespace BankSys.Tests.Entities;

public class ContaBancariaTests
{
    [Fact]
    public void Construtor_DeveCriarContaComSaldoPadraoQuandoSaldoForZero()
    {
        // Arrange
        var nome = "Rafael";
        var documento = "12345678900";

        // Act
        var conta = new ContaBancaria(nome, documento, 0);

        // Assert
        conta.NomeTitular.Should().Be(nome);
        conta.DocumentoTitular.Should().Be(documento);
        conta.Saldo.Should().Be(1000m);
        conta.Status.Should().Be(EnumStatusContaBancaria.Ativa);
    }

    [Fact]
    public void Construtor_DeveSomarSaldoInicialAoSaldoPadrao()
    {
        // Arrange
        var saldoInicial = 500m;

        // Act
        var conta = new ContaBancaria("Rafael", "123", saldoInicial);

        // Assert
        conta.Saldo.Should().Be(1500m);
    }

    [Fact]
    public void InativarConta_DeveAlterarStatusParaInativa()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", decimal.Zero);

        // Act
        conta.InativarConta();

        // Assert
        conta.Status.Should().Be(EnumStatusContaBancaria.Inativa);
    }

    [Fact]
    public void InativarConta_ContaJaInativa_DeveLancarExcecao()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", decimal.Zero);
        conta.InativarConta();

        // Act
        Action act = () => conta.InativarConta();

        // Assert
        act.Should().Throw<ContaBancariaException>()
           .WithMessage("Conta bancária já está inativa");
    }

    [Fact]
    public void Debitar_ComSaldoSuficiente_DeveDiminuirSaldo()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", 500m);

        // Act
        conta.Debitar(300m);

        // Assert
        conta.Saldo.Should().Be(1200m);
    }

    [Fact]
    public void Debitar_ComSaldoInsuficiente_DeveLancarExcecao()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", decimal.Zero);

        // Act
        Action act = () => conta.Debitar(2000m);

        // Assert
        act.Should().Throw<ContaBancariaException>()
           .WithMessage("Saldo Insuficiente");
    }

    [Fact]
    public void Debitar_ContaInativa_DeveLancarExcecao()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", decimal.Zero);
        conta.InativarConta();

        // Act
        Action act = () => conta.Debitar(100m);

        // Assert
        act.Should().Throw<ContaBancariaException>()
           .WithMessage($"Conta bancária de documento {conta.DocumentoTitular} está inativa");
    }

    [Fact]
    public void Creditar_ContaAtiva_DeveAumentarSaldo()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", decimal.Zero);

        // Act
        conta.Creditar(500m);

        // Assert
        conta.Saldo.Should().Be(1500m);
    }

    [Fact]
    public void Creditar_ContaInativa_DeveLancarExcecao()
    {
        // Arrange
        var conta = new ContaBancaria("Rafael", "123", decimal.Zero);
        conta.InativarConta();

        // Act
        Action act = () => conta.Creditar(100m);

        // Assert
        act.Should().Throw<ContaBancariaException>();
    }
}
