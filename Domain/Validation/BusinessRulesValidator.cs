using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Support.Models;

namespace Domain.Validation;

public class BusinessRulesValidator : IBusinessRulesValidator
{
    private const string Symbols = "@#$%^&*()_+";
    private bool IsPasswordStrong(string password)
    {
        return password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(c => Symbols.Contains(c));
    }

    private AppDbContext _context;
    
    public BusinessRulesValidator(AppDbContext context)
    {
        _context = context;
    }
     
    
    public void ValidateBusinessRules(Client client)
    {
        if (_context.Set<Client>().Any(c => c.Email == client.Email || c.Email == client.Email))
        {
            throw new ClientWithThisEmailOrPasswordAlreadyExistsException();
        }
        

        if (client.Dni is < 10000000 or > 99999999)
        {
            throw new InvalidDniException(client.Dni);
        }

        if (!client.Email.Contains('@') || !client.Email.Contains('.') || !IsPasswordStrong(client.Password))
        {
            throw new InvalidEmailOrPasswordException();
        }

        if (client.Phone.Length < 9)
        {
            throw new InvalidPhoneNumberException(client.Phone);
        }

        if (_context.Set<Client>().Any(c => c.Username == client.Username))
        {
            throw new UsernameAlreadyExistsException(client.Username);
        }
    }

    public void ValidateBusinessRules(Owner owner)
    {
        if (owner.Dni is < 10000000 or > 99999999)
        {
            throw new InvalidDniException(owner.Dni);
        }
        if (owner.Phone.Length < 9)
        {
            throw new InvalidPhoneNumberException(owner.Phone);
        }
        
    }

    public void ValidateBusinessRules(Vehicle vehicle)
    {
        if (_context.Set<Vehicle>().Count() >= 14)
        {
            throw new VehicleWithMaxCylindersException();
        }
        
    }

    public void ValidateBusinessRules(OperationCenter operationCenter)
    {
        if (operationCenter.Phone.Length < 9)
        {
            throw new InvalidPhoneNumberException(operationCenter.Phone);
        }
    }
}