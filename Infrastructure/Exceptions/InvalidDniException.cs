namespace Infrastructure.Exceptions;

public class InvalidDniException : Exception
{
    public InvalidDniException(int dni) : base($"Invalid DNI: {dni}")
    {
    }
}