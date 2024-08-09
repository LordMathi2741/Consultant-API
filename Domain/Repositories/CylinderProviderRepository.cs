using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Factory.Cylinder;

namespace Domain.Repositories;

public class CylinderProviderRepository(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<CylinderProvider>(context), ICylinderProviderRepository
{
    public async Task<CylinderProvider> AddCylinderProviderAsync(CylinderProvider cylinderProvider)
    {
        businessRulesValidator.ValidateBusinessRules(cylinderProvider);
        await context.Set<CylinderProvider>().AddAsync(cylinderProvider);
        await unitOfWork.CompleteAsync();
        return cylinderProvider;
    }

    public async Task<CylinderProvider?> UpdateCylinderProviderAsync(CylinderProvider cylinderProvider)
    {
        businessRulesValidator.ValidateBusinessRules(cylinderProvider);
        context.Set<CylinderProvider>().Update(cylinderProvider);
        await unitOfWork.CompleteAsync();
        return cylinderProvider;
    }

    public async Task DeleteCylinderProviderAsync(CylinderProvider cylinderProvider)
    {
        context.Set<CylinderProvider>().Remove(cylinderProvider);
        await unitOfWork.CompleteAsync();
    }
}