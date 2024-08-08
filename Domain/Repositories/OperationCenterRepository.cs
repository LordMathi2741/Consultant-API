using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class OperationCenterRepository(AppDbContext context, IUnitOfWork unitOfWork) : BaseRepository<OperationCenter>(context), IOperationCenterRepository
{
    public async Task<OperationCenter> AddOperationCenterAsync(OperationCenter operationCenter)
    {
        await context.Set<OperationCenter>().AddAsync(operationCenter);
        await unitOfWork.CompleteAsync();
        return operationCenter;
    }

    public async Task<OperationCenter?> UpdateOperationCenterAsync(OperationCenter operationCenter)
    {
        context.Set<OperationCenter>().Update(operationCenter);
        await unitOfWork.CompleteAsync();
        return operationCenter;
    }

    public async Task DeleteOperationCenterAsync(OperationCenter operationCenter)
    {
        context.Set<OperationCenter>().Remove(operationCenter);
        await unitOfWork.CompleteAsync();
    }
}