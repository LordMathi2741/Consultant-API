using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class VehicleRepository(IUnitOfWork unitOfWork, AppDbContext context, IBusinessRulesValidator businessRulesValidator ) : BaseRepository<Vehicle>(context), IVehicleRepository
{
    public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
    {
        businessRulesValidator.ValidateBusinessRules(vehicle);
        await context.Set<Vehicle>().AddAsync(vehicle);
        await unitOfWork.CompleteAsync();
        return vehicle;
    }

    public async Task<Vehicle?> UpdateVehicleAsync(Vehicle vehicle)
    {
        businessRulesValidator.ValidateBusinessRules(vehicle);
        context.Set<Vehicle>().Update(vehicle);
        await unitOfWork.CompleteAsync();
        return vehicle;
    }

    public async Task DeleteVehicle(Vehicle vehicle)
    {
        
        context.Set<Vehicle>().Remove(vehicle); 
        await unitOfWork.CompleteAsync();
    }
    
}