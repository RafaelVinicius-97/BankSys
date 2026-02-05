using BankSys.Application.Dtos;
using BankSys.Application.Interfaces;
using BankSys.Domain.Exceptions;
using BankSys.Domain.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BankSys.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaBancariaController : ControllerBase
{
    private readonly IContaBancariaService _service;
    public ContaBancariaController(IContaBancariaService service)
    {
        _service = service;
    }

    [HttpPost(nameof(CadastrarContaBancaria))]
    [ProducesResponseType(typeof(CriarContaBancariaDto), 201)]
    [ProducesResponseType(400)]
    public IActionResult CadastrarContaBancaria([FromBody] CriarContaBancariaDto dto)
    {
        try
        {
            _service.CadastrarNovaConta(dto);
        return Created();
        }
        catch (ContaBancariaException ex)
        {
            return BadRequest(ex.Message);
}
    }

    [HttpGet(nameof(ConsultarConta))]
    [ProducesResponseType(typeof(IEnumerable<ConsultarContaBancariaDto>), 200)]
    [ProducesResponseType(404)]
    public IActionResult ConsultarConta([FromQuery] ConsultarContasBancariasFilter filter)
    {
        return Ok(_service.ConsultarContasBancarias(filter));
    }

    [HttpDelete(nameof(InativarConta))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult InativarConta([FromBody] string documentoTitular)
    {
        try
        {
            return Ok(_service.InativarConta(documentoTitular));
        }
        catch (ContaBancariaException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(nameof(RealizarTransferenciaBancaria))]
    [ProducesResponseType(typeof(RealizarTranferenciaBancariaDto), 201)]
    [ProducesResponseType(400)]
    public IActionResult RealizarTransferenciaBancaria([FromBody] RealizarTranferenciaBancariaDto dto)
    {
        try
        {
            _service.RealizarTransferencia(dto);
            return NoContent();
        }
        catch (ContaBancariaException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
