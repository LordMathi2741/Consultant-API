using Infrastructure.Interfaces;
using Support.Factory.Cylinder;
using Support.Models;

namespace Domain.Interfaces;

public interface IWorkShopCylinderRepository : IBaseRepository<WorkShopCylinder>
{
    Task<WorkShopCylinder> AddWorkShopCylinderAsync(WorkShopCylinder workShopCylinder);
    Task<WorkShopCylinder?> UpdateWorkShopCylinderAsync(WorkShopCylinder workShopCylinder);
    Task DeleteWorkShopCylinderAsync(WorkShopCylinder workShopCylinder);
}