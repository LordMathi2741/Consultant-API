namespace Infrastructure.Exceptions;

public class ClientWithThisEmailOrPasswordAlreadyExistsException : Exception
{
    public ClientWithThisEmailOrPasswordAlreadyExistsException() : base("Client with this email and password already exists")
    {
    }
}