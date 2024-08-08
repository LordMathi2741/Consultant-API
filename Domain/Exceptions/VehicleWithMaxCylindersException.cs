namespace Domain.Exceptions;

public class VehicleWithMaxCylindersException : Exception
{
    public VehicleWithMaxCylindersException() : base("This vehicle have more than 4 cylinders.")
    {
    }
}