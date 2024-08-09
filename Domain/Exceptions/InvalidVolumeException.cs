namespace Domain.Exceptions;

public class InvalidVolumeException : Exception
{
    public InvalidVolumeException(long volume) : base("Invalid volume: " + volume + ". Volume must be greater than 0.")
    {
    }
}