using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Support.Factory.Company;
using Support.Factory.Cylinder;
using Support.Models;

namespace Domain.Validation;

public class BusinessRulesValidator : IBusinessRulesValidator
{
    private const string Symbols = "@#$%^&*()_+";
    private readonly AppDbContext _context;
    private bool IsPasswordStrong(string password)
    {
        return password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(c => Symbols.Contains(c));
    }
    
    private bool IsValidPhoneNumber(string phone)
    {
        return phone.Length >= 9;
    }
    
    private bool IsValidDni(int dni)
    {
        return dni is >= 10000000 and <= 99999999;
    }

    private bool IsValidRuc(string ruc)
    {
          return ruc.Length == 11;
    }
    
    private bool IsFutureDate(DateTimeOffset? date)
    {
        return date > DateTimeOffset.Now;
    }
    
    private bool IsFiveYearsOld(DateTimeOffset? date)
    {
        return date < DateTimeOffset.Now.AddYears(-5);
    }
    
    public BusinessRulesValidator(AppDbContext context)
    {
        _context = context;
    }
     
    
    public void ValidateBusinessRules(User user)
    {
        if (_context.Set<User>().Any(c => c.Email == user.Email || c.Email == user.Email))
        {
            throw new ClientWithThisEmailOrPasswordAlreadyExistsException();
        }
        

        if (!IsValidDni(user.Dni))
        {
            throw new InvalidDniException(user.Dni);
        }

        if (!user.Email.Contains('@') || !user.Email.Contains('.') || !IsPasswordStrong(user.Password))
        {
            throw new InvalidEmailOrPasswordException();
        }

        if (!IsValidPhoneNumber(user.Phone))
        {
            throw new InvalidPhoneNumberException(user.Phone);
        }

        if (_context.Set<User>().Any(c => c.Username == user.Username))
        {
            throw new UsernameAlreadyExistsException(user.Username);
        }
    }

    public void ValidateBusinessRules(Owner owner)
    {
        if (!IsValidDni(owner.Dni))
        {
            throw new InvalidDniException(owner.Dni);
        }
        if (!IsValidPhoneNumber(owner.Phone))
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

    public void ValidateBusinessRules(Cylinder cylinder)
    {
        if (cylinder.Capacity < 0)
        {
            throw new InvalidCapacityException(cylinder.Capacity);
        }
       
    }

    public void ValidateBusinessRules(OperationCenter operationCenter)
    {
        if (!IsValidPhoneNumber(operationCenter.Phone))
        {
            throw new InvalidPhoneNumberException(operationCenter.Phone);
        }
    }

    public void ValidateBusinessRules(CylinderProvider cylinderProvider)
    {
        if (_context.Set<CylinderProvider>().Count() >= 250)
        {
            throw new MaxCylinderProvidersException();
        }
        
        if(IsFutureDate(cylinderProvider.EmitDate))
        {
            throw new FutureDateException(cylinderProvider.EmitDate);
        }

        if (cylinderProvider.Volume < 0)
        {
            throw new InvalidVolumeException(cylinderProvider.Volume);
        }
    }

    public void ValidateBusinessRules(InstallerCompany installerCompany)
    {
        if (!IsValidRuc(installerCompany.Ruc))
        {
            throw new InvalidRucException(installerCompany.Ruc);
        }
    }

    public void ValidateBusinessRules(ProviderCompany providerCompany)
    {
        if (!IsValidRuc(providerCompany.Ruc))
        {
            throw new InvalidRucException(providerCompany.Ruc);
        }
        if(!IsValidPhoneNumber(providerCompany.Phone))
        {
            throw new InvalidPhoneNumberException(providerCompany.Phone);
        }
    }

    public void ValidateBusinessRules(WorkShopCompany workShopCompany)
    {
        if (!IsValidRuc(workShopCompany.Ruc))
        {
            throw new InvalidRucException(workShopCompany.Ruc);
        }
        if(!IsValidPhoneNumber(workShopCompany.Phone))
        {
            throw new InvalidPhoneNumberException(workShopCompany.Phone);
        }
    }

    public void ValidateBusinessRules(WorkShop workShop)
    {
        if (!IsValidPhoneNumber(workShop.Phone))
        {
            throw new InvalidPhoneNumberException(workShop.Phone);
        }

        if (_context.Set<WorkShop>().Any(w => w.WorkShopCompanyId == workShop.WorkShopCompanyId))
        {
            if(!IsFiveYearsOld(DateTimeOffset.Now))
            {
                throw new WorkShopCannotRegisterIFiveYearsException();
            }
        }
    }

    public void ValidateBusinessRules(WorkShopCylinder workShopCylinder)
    {
        if(IsFutureDate(workShopCylinder.MadeDate))
        {
            throw new FutureDateException(workShopCylinder.MadeDate);
        }

        if (workShopCylinder.Volume < 0)
        {
            throw new InvalidVolumeException(workShopCylinder.Volume);
        }

        if (_context.Set<WorkShopCylinder>().Count() >= 250)
        {
            throw new MaxWorkShopCylindersException();
        }
    }
}