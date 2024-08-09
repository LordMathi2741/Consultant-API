namespace Domain.Exceptions;

public class InvalidCapacityException : Exception
{
       public InvalidCapacityException(int capacity) : base("Invalid capacity: " + capacity + ". Capacity must be greater than 0.")
        {
        }
}