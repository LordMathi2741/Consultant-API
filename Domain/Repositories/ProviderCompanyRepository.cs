using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Factory.Company;

namespace Domain.Repositories;

public class ProviderCompanyRepository(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<ProviderCompany>(context), IProviderCompanyRepository
{
    public async Task<ProviderCompany> AddProviderCompanyAsync(ProviderCompany providerCompany)
    {
        businessRulesValidator.ValidateBusinessRules(providerCompany);
        await context.Set<ProviderCompany>().AddAsync(providerCompany);
        await unitOfWork.CompleteAsync();
        return providerCompany;
    }

    public async Task<ProviderCompany?> UpdateProviderCompanyAsync(ProviderCompany providerCompany)
    {
        businessRulesValidator.ValidateBusinessRules(providerCompany);
        context.Set<ProviderCompany>().Update(providerCompany);
        await unitOfWork.CompleteAsync();
        return providerCompany;
    }

    public async Task DeleteProviderCompanyAsync(ProviderCompany providerCompany)
    {
        context.Set<ProviderCompany>().Remove(providerCompany);
        await unitOfWork.CompleteAsync();
    }
}