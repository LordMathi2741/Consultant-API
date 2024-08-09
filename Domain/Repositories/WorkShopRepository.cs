using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class WorkShopRepository(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<WorkShop>(context), IWorkShopRepository
{
    public async Task<WorkShop> AddWorkShopAsync(WorkShop workShop)
    {
        businessRulesValidator.ValidateBusinessRules(workShop);
        await context.Set<WorkShop>().AddAsync(workShop);
        await unitOfWork.CompleteAsync();
        return workShop;
    }

    public async Task DeleteWorkShopAsync(WorkShop workShop)
    {
        context.Set<WorkShop>().Remove(workShop);
        await unitOfWork.CompleteAsync();
    }

    public async Task<WorkShop?> UpdateWorkShopAsync(WorkShop workShop)
    {
        businessRulesValidator.ValidateBusinessRules(workShop);
        context.Set<WorkShop>().Update(workShop);
        await unitOfWork.CompleteAsync();
        return workShop;
    }
}