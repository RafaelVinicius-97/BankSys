using BankSys.Application.Dtos;
using BankSys.Application.Services;
using BankSys.Domain.Entities;
using BankSys.Domain.Exceptions;
using BankSys.Domain.Interfaces;
using BankSys.Persistence.UoW;
using FluentAssertions;
using Moq;

namespace BankSys.Tests.Services;

public class ContaBancariaServiceTests
{
    private readonly Mock<IContaBancariaRepository> _contaRepositoryMock;
    private readonly Mock<ITransferenciaBancariaRepository> _transferenciaBancariaMock;
    private readonly Mock<IUoW> _uowMock;
    private readonly ContaBancariaService _service;

    public ContaBancariaServiceTests()
    {
        _contaRepositoryMock = new Mock<IContaBancariaRepository>();
        _transferenciaBancariaMock = new Mock<ITransferenciaBancariaRepository>();
        _uowMock = new Mock<IUoW>();

        _service = new ContaBancariaService(
            _contaRepositoryMock.Object,
            _transferenciaBancariaMock.Object,
            _uowMock.Object
        );
    }

    [Fact]
    public void CriarConta_DeveCriarContaComSaldoInicial()
    {
        // Arrange
        CriarContaBancariaDto novaConta = new("João", "12345", 1000M);

        var contaCriada = new ContaBancaria(
            novaConta.NomeTitular,
            novaConta.DocumentoTitular,
            novaConta.Saldo
        );

        _contaRepositoryMock
            .Setup(r => r.TitularPossuiContaBancariaComEsseDocumento(It.IsAny<string>()))
            .Returns(false);

        _contaRepositoryMock
            .Setup(r => r.CriarConta(It.IsAny<ContaBancaria>()))
            .Returns(contaCriada);

        // Act
        var conta = _service.CadastrarNovaConta(novaConta);

        // Assert
        conta.Should().NotBeNull();
        conta.Saldo.Should().Be(contaCriada.Saldo);

        _uowMock.Verify(u => u.SaveChanges(), Times.Once);
    }


    [Fact]
    public void CriarConta_ComDocumentoDuplicado_DeveLancarExcecao()
    {
        // Arrange
        CriarContaBancariaDto novaConta = new("João", "12345", 1000M);

        _contaRepositoryMock
            .Setup(r => r.TitularPossuiContaBancariaComEsseDocumento(novaConta.DocumentoTitular))
            .Returns(true);

        // Act
        Action act = () => _service.CadastrarNovaConta(novaConta);

        // Assert
        act.Should().Throw<ContaBancariaException>()
           .WithMessage("Titular já possui conta com esse documento");
    }

    [Fact]
    public async Task Transferencia_ComSaldoSuficiente_DeveDebitarECreditar()
    {
        // Arrange
        var contaOrigem = new ContaBancaria("João", "12345", 500M);
        var contaDestino = new ContaBancaria("Maria", "54321", 500M);

        _contaRepositoryMock
            .Setup(r => r.BuscarContaBancaria(contaOrigem.DocumentoTitular))
            .Returns(contaOrigem);

        _contaRepositoryMock
            .Setup(r => r.BuscarContaBancaria(contaDestino.DocumentoTitular))
            .Returns(contaDestino);

        // Act
        _service.RealizarTransferencia(new(contaOrigem.NomeTitular, contaOrigem.DocumentoTitular, contaDestino.DocumentoTitular, 200M));

        // Assert
        contaOrigem.Saldo.Should().Be(1300);
        contaDestino.Saldo.Should().Be(1700);

        _uowMock.Verify(u => u.CommitTransaction(), Times.Once);
    }
}

