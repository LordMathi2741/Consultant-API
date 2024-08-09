using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Factory.Cylinder;

namespace Domain.Repositories;

public class WorkShopCylinderRepository(AppDbContext context,IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<WorkShopCylinder>(context), IWorkShopCylinderRepository
{
    
    public async Task<WorkShopCylinder> AddWorkShopCylinderAsync(WorkShopCylinder workShopCylinder)
    {
        businessRulesValidator.ValidateBusinessRules(workShopCylinder);
        await context.Set<WorkShopCylinder>().AddAsync(workShopCylinder);
        await unitOfWork.CompleteAsync();
        return workShopCylinder;
    }

    public async Task<WorkShopCylinder?> UpdateWorkShopCylinderAsync(WorkShopCylinder workShopCylinder)
    {
        businessRulesValidator.ValidateBusinessRules(workShopCylinder);
        context.Set<WorkShopCylinder>().Update(workShopCylinder);
        await unitOfWork.CompleteAsync();
        return workShopCylinder;
    }

    public async Task DeleteWorkShopCylinderAsync(WorkShopCylinder workShopCylinder)
    {
        context.Set<WorkShopCylinder>().Remove(workShopCylinder);
        await unitOfWork.CompleteAsync();
    }
}