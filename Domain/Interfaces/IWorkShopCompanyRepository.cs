using Infrastructure.Interfaces;
using Support.Factory.Company;

namespace Domain.Interfaces;

public interface IWorkShopCompanyRepository : IBaseRepository<WorkShopCompany>
{
    Task<WorkShopCompany> AddWorkShopCompanyAsync(WorkShopCompany workShopCompany);
    Task DeleteWorkShopCompanyAsync(WorkShopCompany workShopCompany);
    Task<WorkShopCompany?> UpdateWorkShopCompanyAsync(WorkShopCompany workShopCompany);
}