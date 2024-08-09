using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> UpdateVehicleAsync(Vehicle vehicle);
    Task DeleteVehicle(Vehicle vehicle);
    
    
}