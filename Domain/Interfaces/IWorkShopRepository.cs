using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IWorkShopRepository : IBaseRepository<WorkShop>
{
    Task<WorkShop> AddWorkShopAsync(WorkShop workShop);
    Task DeleteWorkShopAsync(WorkShop workShop);
    Task<WorkShop?> UpdateWorkShopAsync(WorkShop workShop);
}