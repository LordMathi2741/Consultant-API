using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IOperationCenterRepository : IBaseRepository<OperationCenter>
{
    Task<OperationCenter> AddOperationCenterAsync(OperationCenter operationCenter);
    Task<OperationCenter?> UpdateOperationCenterAsync(OperationCenter operationCenter);
    Task DeleteOperationCenterAsync(OperationCenter operationCenter);
}