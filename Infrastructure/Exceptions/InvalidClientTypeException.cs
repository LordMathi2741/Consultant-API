namespace Infrastructure.Exceptions;

public class InvalidClientTypeException : Exception
{
    public InvalidClientTypeException() : base("Invalid client type")
    {
    }
}