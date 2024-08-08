using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IObservationRepository : IBaseRepository<Observation>
{
    Task<Observation> AddObservationAsync(Observation observation);
}