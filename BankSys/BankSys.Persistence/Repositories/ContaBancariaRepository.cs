using BankSys.Domain.Entities;
using BankSys.Domain.Filters;
using BankSys.Domain.Interfaces;
using BankSys.Persistence.Context;
using Microsoft.IdentityModel.Tokens;

namespace BankSys.Persistence.Repositories;

public class ContaBancariaRepository : IContaBancariaRepository
{
    private readonly BankSysContext _context;

    public ContaBancariaRepository(BankSysContext context)
    {
        _context = context;
    }

    public ContaBancaria CriarConta(ContaBancaria contaBancaria)
    {
        _context.ContasBancarias.Add(contaBancaria);
        return contaBancaria;
    }

    public IEnumerable<ContaBancaria> ConsultarContasBancarias(ConsultarContasBancariasFilter filter) =>
        _context.ContasBancarias
            .Where(x => (filter.NomeTitular.IsNullOrEmpty() || x.NomeTitular.Contains(filter.NomeTitular!))
                && (filter.DocumentoTitular.IsNullOrEmpty() || x.DocumentoTitular == filter.DocumentoTitular))
            .ToList();

    public bool TitularPossuiContaBancariaComEsseDocumento(string documentoTitular) =>
        _context.ContasBancarias
            .Where(x => x.DocumentoTitular == documentoTitular)
            .Any();

    public ContaBancaria? BuscarContaBancaria(string documentoTitular) =>
        _context.ContasBancarias.Where(x => x.DocumentoTitular.Equals(documentoTitular)).FirstOrDefault();

    public void Dispose() =>
        _context.Dispose();
}
