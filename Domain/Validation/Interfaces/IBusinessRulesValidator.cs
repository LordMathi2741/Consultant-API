using Support.Models;

namespace Domain.Validation.Interfaces;

public interface IBusinessRulesValidator
{
    void ValidateBusinessRules(Client client);
    void ValidateBusinessRules(Owner owner);
    void ValidateBusinessRules(Vehicle vehicle);

    void ValidateBusinessRules(OperationCenter operationCenter);

}