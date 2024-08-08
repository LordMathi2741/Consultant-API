using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IValveRepository : IBaseRepository<Valve>
{
    Task<Valve> AddValveAsync(Valve valve);
    Task<Valve?> UpdateValveAsync(Valve valve);
    Task DeleteValveAsync(Valve valve);
}