using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class ValveRepository(AppDbContext context, IUnitOfWork unitOfWork) : BaseRepository<Valve>(context), IValveRepository
{
    public async Task<Valve> AddValveAsync(Valve valve)
    {
        await context.Set<Valve>().AddAsync(valve);
        await unitOfWork.CompleteAsync();
        return valve;
    }

    public async Task<Valve?> UpdateValveAsync(Valve valve)
    {
        context.Set<Valve>().Update(valve);
        await unitOfWork.CompleteAsync();
        return valve;
    }

    public async Task DeleteValveAsync(Valve valve)
    {
        context.Set<Valve>().Remove(valve);
        await unitOfWork.CompleteAsync();
    }
}