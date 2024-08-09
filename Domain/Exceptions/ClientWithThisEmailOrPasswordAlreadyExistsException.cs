namespace Domain.Exceptions;

public class ClientWithThisEmailOrPasswordAlreadyExistsException : Exception
{
    public ClientWithThisEmailOrPasswordAlreadyExistsException() : base("User with this email and password already exists")
    {
    }
}