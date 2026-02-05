namespace BankSys.Application.Dtos;

public record RealizarTranferenciaBancariaDto(
    string NomeCliente, 
    string DocumentoContaOrigem, 
    string DocumentoContaDestino, 
    decimal Valor);
