using Infrastructure.Interfaces;
using Support.Factory.Cylinder;

namespace Domain.Interfaces;

public interface ICylinderProviderRepository : IBaseRepository<CylinderProvider>
{
    Task<CylinderProvider> AddCylinderProviderAsync(CylinderProvider cylinderProvider);
    Task<CylinderProvider?> UpdateCylinderProviderAsync(CylinderProvider cylinderProvider);
    Task DeleteCylinderProviderAsync(CylinderProvider cylinderProvider);
}