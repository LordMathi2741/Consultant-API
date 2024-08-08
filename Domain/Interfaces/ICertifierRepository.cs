using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface ICertifierRepository : IBaseRepository<Certifier>
{
    Task<Certifier> AddCertifierAsync(Certifier certifier);
    Task<Certifier?> UpdateCertifierAsync(Certifier certifier);
    Task DeleteCertifierAsync(Certifier certifier);
}