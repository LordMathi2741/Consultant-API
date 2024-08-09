using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface ICylinderRepository : IBaseRepository<Cylinder>
{
    Task<Cylinder> AddCylinderAsync(Cylinder cylinder);
    Task<Cylinder?> UpdateCylinderAsync(Cylinder cylinder);
    Task DeleteCylinderAsync(Cylinder cylinder);
    
}