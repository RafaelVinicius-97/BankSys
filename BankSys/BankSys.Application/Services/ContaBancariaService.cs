using BankSys.Application.Dtos;
using BankSys.Application.Interfaces;
using BankSys.Application.Mappers;
using BankSys.Domain.Entities;
using BankSys.Domain.Exceptions;
using BankSys.Domain.Filters;
using BankSys.Domain.Interfaces;
using BankSys.Persistence.UoW;

namespace BankSys.Application.Services;

public class ContaBancariaService : IContaBancariaService
{
    private readonly IContaBancariaRepository _repository;
    private readonly ITransferenciaBancariaRepository _tranferenciaRepository;
    private readonly IUoW _uoW;
    public ContaBancariaService(
        IContaBancariaRepository repository, 
        ITransferenciaBancariaRepository tranferenciaRepository,
        IUoW uoW)
    {
        _repository = repository;
        _tranferenciaRepository = tranferenciaRepository;
        _uoW = uoW;
    }

    public CriarContaBancariaDto CadastrarNovaConta(CriarContaBancariaDto dto)
    {
        if (_repository.TitularPossuiContaBancariaComEsseDocumento(dto.DocumentoTitular))
            throw new ContaBancariaException("Titular já possui conta com esse documento");

        var contaCadastrada = _repository.CriarConta(new(dto.NomeTitular, dto.DocumentoTitular, dto.Saldo ?? decimal.Zero));
        _uoW.SaveChanges();

        return contaCadastrada.ConvertToCriarContaBancariaDto();
    }

    public IEnumerable<ConsultarContaBancariaDto> ConsultarContasBancarias(ConsultarContasBancariasFilter filter)
    {
        var listagem = _repository.ConsultarContasBancarias(filter);

        return listagem.ConvertToDto();
    }
    
    public bool InativarConta(string documentoTitular)
    {
        ContaBancaria contaBancaria = _repository.BuscarContaBancaria(documentoTitular) 
            ?? throw new ContaBancariaException("Conta não existe");

        contaBancaria.InativarConta();

        _uoW.SaveChanges();

        return true;
    }

    public void RealizarTransferencia(RealizarTranferenciaBancariaDto dto)
    {
        try
        {
            _uoW.BeginTransaction();

            var origem = _repository.BuscarContaBancaria(dto.DocumentoContaOrigem)
                ?? throw new ContaBancariaException("Conta de origem não encontrada");
            var destino = _repository.BuscarContaBancaria(dto.DocumentoContaDestino)
                ?? throw new ContaBancariaException("Conta de destino não encontrada");

            origem.ContaValida();
            destino.ContaValida();
            
            origem.Debitar(dto.Valor);
            destino.Creditar(dto.Valor);

            TransferenciaBancaria tranferencia = new(origem.Id, origem.NomeTitular, destino.Id, dto.Valor);

            _tranferenciaRepository.AdicionarTransferencia(tranferencia);

            _uoW.CommitTransaction();
        }
        catch
        {
            _uoW.RollbackTransaction();
            throw;
        }
    }
}
