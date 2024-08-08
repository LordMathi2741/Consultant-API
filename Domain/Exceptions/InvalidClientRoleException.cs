namespace Domain.Exceptions;

public class InvalidClientRoleException : Exception
{
    public InvalidClientRoleException() : base("Invalid client role.")
    {
    }
}