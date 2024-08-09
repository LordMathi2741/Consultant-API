using System.Net;
using Domain.Exceptions;
using Newtonsoft.Json;

namespace Application.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        { await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        if (exception is ClientWithThisEmailOrPasswordAlreadyExistsException or UsernameAlreadyExistsException or VehicleWithMaxCylindersException or MaxCylinderProvidersException or MaxWorkShopCylindersException or WorkShopCannotRegisterIFiveYearsException)
        {
            code = HttpStatusCode.Conflict;
        }
        else if (exception is  InvalidDniException or InvalidPhoneNumberException or InvalidEmailOrPasswordException or FutureDateException or InvalidCapacityException or InvalidClientRoleException or InvalidVolumeException or InvalidRucException )
        {
            code = HttpStatusCode.BadRequest;
        }
        else if(exception is UserNotFoundException)
        {
            code = HttpStatusCode.NotFound;
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}