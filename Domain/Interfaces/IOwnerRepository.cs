using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IOwnerRepository : IBaseRepository<Owner>
{
    Task<Owner> AddOwnerAsync(Owner owner);
    Task<Owner?> UpdateOwnerAsync(Owner owner);
    Task DeleteOwnerAsync(Owner owner);
}