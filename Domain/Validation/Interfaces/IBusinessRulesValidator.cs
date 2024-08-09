using Support.Factory.Company;
using Support.Factory.Cylinder;
using Support.Models;

namespace Domain.Validation.Interfaces;

public interface IBusinessRulesValidator
{
    void ValidateBusinessRules(User user);
    void ValidateBusinessRules(Owner owner);
    void ValidateBusinessRules(Vehicle vehicle);
    
    void ValidateBusinessRules(Cylinder cylinder);

    void ValidateBusinessRules(OperationCenter operationCenter);

    void ValidateBusinessRules(CylinderProvider cylinderProvider);
    
    void ValidateBusinessRules(InstallerCompany installerCompany);
    
    void ValidateBusinessRules(ProviderCompany providerCompany);
    
    void ValidateBusinessRules(WorkShopCompany workShopCompany);
    
    void ValidateBusinessRules(WorkShop workShop);
    
    void ValidateBusinessRules(WorkShopCylinder workShopCylinder);

}