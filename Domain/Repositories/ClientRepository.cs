using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Validation.Interfaces;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;
using Support.Models;
using Client = Support.Models.Client;

namespace Domain.Repositories;

public class ClientRepository(AppDbContext context, IUnitOfWork unitOfWork, ITokenService tokenService, IEncryptService encryptService, IBusinessRulesValidator businessRulesValidator ) : BaseRepository<Client>(context), IClientRepository
{
   
    
    
    public async Task<Client> SignUp(Client client)
    {
        businessRulesValidator.ValidateBusinessRules(client);
        var clientHashed = new Client
        {
            Username = client.Username,
            Password = encryptService.Encrypt(client.Password),
            Firstname = client.Firstname,
            Lastname = client.Lastname,
            Email = client.Email,
            Phone = client.Phone,
            Dni = client.Dni,
            Role = client.Role,
            Address = client.Address,
            Company = client.Company,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };
        await context.Set<Client>().AddAsync(clientHashed);
        await unitOfWork.CompleteAsync();
        return client;
    }

    public async Task<string> SignIn(string email, string password)
    {
        var client = await context.Set<Client>().FirstOrDefaultAsync(c => c.Email == email);
        if (client == null) throw new UserNotFoundException();
        if (!encryptService.Verify(password, client.Password)) throw new UserNotFoundException();
        var token = tokenService.GenerateToken(client);
        return token;
    }
    

    public async Task<Client?> UpdateClient(Client client)
    {
        businessRulesValidator.ValidateBusinessRules(client);
        client.UpdatedDate = DateTime.Now;
        context.Set<Client>().Update(client);
        await unitOfWork.CompleteAsync();
        return client;
    }

    public async Task<Client?> UpdateClientRole(Client client, string role)
    {
        if (!Enum.IsDefined(typeof(EClientRoles), role))
        {
            throw new InvalidClientRoleException();
        }
        client.Role = role;
        context.Set<Client>().Update(client);
        await unitOfWork.CompleteAsync();
        return client;
    }


    public async Task DeleteClient(Client client)
    {
        context.Set<Client>().Remove(client);
        await unitOfWork.CompleteAsync();
    }
}