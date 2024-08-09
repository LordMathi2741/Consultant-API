using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Factory.Company;

namespace Domain.Repositories;

public class InstallerCompanyRepository(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<InstallerCompany>(context), IInstallerCompanyRepository
{
    public async Task<InstallerCompany> AddInstallerCompanyAsync(InstallerCompany installerCompany)
    {
        businessRulesValidator.ValidateBusinessRules(installerCompany);
        await context.Set<InstallerCompany>().AddAsync(installerCompany);
        await unitOfWork.CompleteAsync();
        return installerCompany;
    }

    public async Task DeleteInstallerCompanyAsync(InstallerCompany installerCompany)
    {
        context.Set<InstallerCompany>().Remove(installerCompany);
        await unitOfWork.CompleteAsync();
    }

    public async Task<InstallerCompany?> UpdateInstallerCompanyAsync(InstallerCompany installerCompany)
    {
        businessRulesValidator.ValidateBusinessRules(installerCompany);
        context.Set<InstallerCompany>().Update(installerCompany);
        await unitOfWork.CompleteAsync();
        return installerCompany;
    }
}