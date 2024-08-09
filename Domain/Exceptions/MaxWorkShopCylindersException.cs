namespace Domain.Exceptions;

public class MaxWorkShopCylindersException : Exception
{
    public MaxWorkShopCylindersException() : base("The workshop has reached the maximum number of cylinders.")
    {
    }
}