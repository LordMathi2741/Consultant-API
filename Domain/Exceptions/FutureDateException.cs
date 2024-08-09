namespace Domain.Exceptions;

public class FutureDateException : Exception
{
    public FutureDateException(DateTimeOffset? date) : base($"Date {date} is in the future")
    {
    }
}