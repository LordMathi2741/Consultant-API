namespace Domain.Exceptions;

public class WorkShopCannotRegisterIFiveYearsException : Exception
{
    public WorkShopCannotRegisterIFiveYearsException() : base("This workshop already registered, please try again in five years.")
    {
    }

    
}