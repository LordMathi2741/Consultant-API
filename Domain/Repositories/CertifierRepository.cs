using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class CertifierRepository(AppDbContext context, IUnitOfWork unitOfWork) : BaseRepository<Certifier>(context), ICertifierRepository
{
    public async Task<Certifier> AddCertifierAsync(Certifier certifier)
    {
        await context.Set<Certifier>().AddAsync(certifier);
        await unitOfWork.CompleteAsync();
        return certifier;
    }

    public async Task<Certifier?> UpdateCertifierAsync(Certifier certifier)
    {
        context.Set<Certifier>().Update(certifier);
        await unitOfWork.CompleteAsync();
        return certifier;
    }

    public async Task DeleteCertifierAsync(Certifier certifier)
    {
        context.Set<Certifier>().Remove(certifier);
        await unitOfWork.CompleteAsync();
    }
}