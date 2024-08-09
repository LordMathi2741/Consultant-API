namespace Domain.Exceptions;

public class MaxCylinderProvidersException : Exception
{
    public MaxCylinderProvidersException() : base("Max number of cylinder providers reached.")
    {
    }
}