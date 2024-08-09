using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Support.Factory.Cylinder;
using Support.Models;

namespace Domain.Repositories;

public class CylinderRepository(AppDbContext context, IUnitOfWork unitOfWork) : BaseRepository<Cylinder>(context), ICylinderRepository
{
    public async Task<Cylinder> AddCylinderAsync(Cylinder cylinder)
    {
        
        await context.Set<Cylinder>().AddAsync(cylinder);
        await unitOfWork.CompleteAsync();
        return cylinder;
    }

    public async Task<Cylinder?> UpdateCylinderAsync(Cylinder cylinder)
    {
        context.Set<Cylinder>().Update(cylinder);
        await unitOfWork.CompleteAsync();
        return cylinder;
    }

    public async Task DeleteCylinderAsync(Cylinder cylinder)
    {
        context.Set<Cylinder>().Remove(cylinder);
        await unitOfWork.CompleteAsync();
    }
    
}