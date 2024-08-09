using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Factory.Company;

namespace Domain.Repositories;

public class WorkShopCompanyRepository(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator): BaseRepository<WorkShopCompany>(context), IWorkShopCompanyRepository
{
    public async Task<WorkShopCompany> AddWorkShopCompanyAsync(WorkShopCompany workShopCompany)
    {
        businessRulesValidator.ValidateBusinessRules(workShopCompany);
        await context.Set<WorkShopCompany>().AddAsync(workShopCompany);
        await unitOfWork.CompleteAsync();
        return workShopCompany;
    }

    public async Task DeleteWorkShopCompanyAsync(WorkShopCompany workShopCompany)
    {
        context.Set<WorkShopCompany>().Remove(workShopCompany);
        await unitOfWork.CompleteAsync();
    }

    public async Task<WorkShopCompany?> UpdateWorkShopCompanyAsync(WorkShopCompany workShopCompany)
    {
        businessRulesValidator.ValidateBusinessRules(workShopCompany);
        context.Set<WorkShopCompany>().Update(workShopCompany);
        await unitOfWork.CompleteAsync();
        return workShopCompany;
    }
}