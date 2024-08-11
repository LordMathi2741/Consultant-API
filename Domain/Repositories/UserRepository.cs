using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;
using Support.Models;

namespace Domain.Repositories;

public class UserRepository(AppDbContext context, IUnitOfWork unitOfWork, ITokenService tokenService, IEncryptService encryptService, IBusinessRulesValidator businessRulesValidator ) : BaseRepository<User>(context), IUserRepository
{
   
    
    
    public async Task<User> SignUp(User user)
    {
        businessRulesValidator.ValidateBusinessRules(user);
        var clientHashed = new User
        {
            Username = user.Username,
            Password = encryptService.Encrypt(user.Password),
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Email = user.Email,
            Phone = user.Phone,
            Dni = user.Dni,
            Role = user.Role,
            Address = user.Address,
            Company = user.Company,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        await context.Set<User>().AddAsync(clientHashed);
        await unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<string> SignIn(string email, string password)
    {
        var client = await context.Set<User>().FirstOrDefaultAsync(c => c.Email == email);
        if (client == null) throw new UserNotFoundException();
        if (!encryptService.Verify(password, client.Password)) throw new UserNotFoundException();
        var token = tokenService.GenerateToken(client);
        return token;
    }

    public async Task<User?> GetClientByEmail(string email)
    {
        return await context.Set<User>().FirstOrDefaultAsync(c => c.Email == email);
    }


    public async Task<User?> UpdateClient(User user)
    {
        businessRulesValidator.ValidateBusinessRules(user);
        user.UpdatedDate = DateTime.Now;
        context.Set<User>().Update(user);
        await unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<User?> UpdateClientRole(User user, string role)
    {
        if (!Enum.IsDefined(typeof(EClientRoles), role))
        {
            throw new InvalidClientRoleException();
        }
        user.Role = role;
        context.Set<User>().Update(user);
        await unitOfWork.CompleteAsync();
        return user;
    }


    public async Task DeleteClient(User user)
    {
        context.Set<User>().Remove(user);
        await unitOfWork.CompleteAsync();
    }
}