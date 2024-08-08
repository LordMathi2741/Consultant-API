namespace Infrastructure.Exceptions;

public class InvalidPhoneNumberException : Exception
{
    public InvalidPhoneNumberException(string phoneNumber) : base($"Invalid phone number: {phoneNumber}")
    {
    }
}