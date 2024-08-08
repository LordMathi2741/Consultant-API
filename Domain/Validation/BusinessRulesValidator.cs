using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Support.Models;

namespace Domain.Validation;

public class BusinessRulesValidator : IBusinessRulesValidator
{
    private const string Symbols = "@#$%^&*()_+";
    private bool IsPasswordStrong(string password)
    {
        return password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(c => Symbols.Contains(c));
    }
    
    private readonly IClientRepository _clientRepository;
    private readonly ICylinderRepository _cylinderRepository;
    
    public void ValidateBusinessRules(Client client)
    {
        if (_clientRepository.ClientWithEmailOrPasswordExists(client.Email, client.Password))
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

        if (_clientRepository.ClientWithThisUsernameExists(client.Username))
        {
            throw new UsernameAlreadyExistsException(client.Username);
        }
        if (_clientRepository.ClientWithEmailOrPasswordExists(client.Email, client.Password))
        {
            throw new ClientWithThisEmailOrPasswordAlreadyExistsException();
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

    public void ValidateBusinessRules(Cylinder cylinder)
    {
        if (_cylinderRepository.CountCylindersByVehicleIdAsync(cylinder.VehicleId).Result >= 4)
        {
            throw new VehicleWithMaxCylindersException();
        }
    }
}