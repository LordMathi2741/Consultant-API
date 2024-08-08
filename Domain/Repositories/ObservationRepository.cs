using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class ObservationRepository(AppDbContext context, IUnitOfWork unitOfWork) : BaseRepository<Observation>(context), IObservationRepository
{
    public async Task<Observation> AddObservationAsync(Observation observation)
    {
        await context.Set<Observation>().AddAsync(observation);
        await unitOfWork.CompleteAsync();
        return observation;
    }
}