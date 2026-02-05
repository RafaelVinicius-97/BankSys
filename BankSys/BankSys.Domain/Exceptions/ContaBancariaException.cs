namespace BankSys.Domain.Exceptions;

public class ContaBancariaException : Exception
{
    public ContaBancariaException(string message) : base(message)
    { }
}
