using Infrastructure.Interfaces;
using Support.Factory.Company;

namespace Domain.Interfaces;

public interface IInstallerCompanyRepository : IBaseRepository<InstallerCompany>
{
    Task<InstallerCompany> AddInstallerCompanyAsync(InstallerCompany installerCompany);
    Task DeleteInstallerCompanyAsync(InstallerCompany installerCompany);
    Task<InstallerCompany?> UpdateInstallerCompanyAsync(InstallerCompany installerCompany);
}