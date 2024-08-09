using Infrastructure.Interfaces;
using Support.Factory.Company;

namespace Domain.Interfaces;

public interface IProviderCompanyRepository : IBaseRepository<ProviderCompany>
{
    Task<ProviderCompany> AddProviderCompanyAsync(ProviderCompany providerCompany);
    Task<ProviderCompany?> UpdateProviderCompanyAsync(ProviderCompany providerCompany);
    Task DeleteProviderCompanyAsync(ProviderCompany providerCompany);
}