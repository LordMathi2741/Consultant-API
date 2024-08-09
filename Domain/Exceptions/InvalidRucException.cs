namespace Domain.Exceptions;

public class InvalidRucException : Exception
{
    public InvalidRucException(string ruc) : base($"The RUC {ruc} is invalid")
    {
    }
}