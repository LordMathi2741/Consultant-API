using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Support.Models;

namespace Domain.Repositories;

public class OwnerRepository(AppDbContext context, IUnitOfWork unitOfWork, IBusinessRulesValidator businessRulesValidator) : BaseRepository<Owner>(context), IOwnerRepository
{
    public async Task<Owner> AddOwnerAsync(Owner owner)
    {
        businessRulesValidator.ValidateBusinessRules(owner);
        await context.Set<Owner>().AddAsync(owner);
        await unitOfWork.CompleteAsync();
        return owner;
    }

    public async Task<Owner?> UpdateOwnerAsync(Owner owner)
    {
        businessRulesValidator.ValidateBusinessRules(owner);
        context.Set<Owner>().Update(owner);
        await unitOfWork.CompleteAsync();
        return owner;
    }

    public async Task DeleteOwnerAsync(Owner owner)
    {
        context.Set<Owner>().Remove(owner);
        await unitOfWork.CompleteAsync();
    }
}