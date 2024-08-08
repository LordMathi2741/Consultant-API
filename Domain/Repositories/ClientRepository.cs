using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Security.Interfaces;

namespace Domain.Repositories;

public class ClientRepository(AppDbContext context, IUnitOfWork unitOfWork, ITokenService tokenService, IEncryptService encryptService ) : BaseRepository<Client>(context), IClientRepository
{
    private const string Symbols = "@#$%^&*()_+";
    private bool IsPasswordStrong(string password)
    {
        return password.Any(char.IsDigit) && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(c => Symbols.Contains(c));
    }

    private void ValidateBusinessRules(Client client)
    {
        if (ClientWithEmailOrPasswordExists(client.Email, client.Password))
        {
            throw new ClientWithThisEmailOrPasswordAlreadyExistsException();
        }

        if (!Enum.IsDefined(typeof(EClientTypes), client.Type))
        {
            throw new InvalidClientTypeException();
        }

        if (client.Dni is < 10000000 or > 99999999)
        {
            throw new InvalidDniException(client.Dni);
        }

        if (!client.Email.Contains('@') || !client.Email.Contains('.') || !IsPasswordStrong(client.Password))
        {
            throw new InvalidEmailOrPasswordException();
        }

        if (client.Phone.Length < 9)
        {
            throw new InvalidPhoneNumberException(client.Phone);
        }

        if (ClientWithThisUsernameExists(client.Username))
        {
            throw new UsernameAlreadyExistsException(client.Username);
        }
    }
    public async Task<Client> SignUp(Client client)
    {
        ValidateBusinessRules(client);
        var clientHashed = new Client
        {
            Username = client.Username,
            Password = encryptService.Encrypt(client.Password),
            Firstname = client.Firstname,
            Lastname = client.Lastname,
            Email = client.Email,
            Phone = client.Phone,
            Dni = client.Dni,
            Type = client.Type,
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

    public bool ClientWithEmailOrPasswordExists(string email, string password)
    {
        return context.Set<Client>().Any(c => c.Email == email && c.Password == password);
    }

    public bool ClientWithThisUsernameExists(string username)
    {
        return context.Set<Client>().Any(c => c.Username == username);
    }

    public async Task<Client?> UpdateClient(Client client)
    {
        ValidateBusinessRules(client);
        if (ClientWithEmailOrPasswordExists(client.Email, client.Password))
        {
            throw new ClientWithThisEmailOrPasswordAlreadyExistsException();
        }
        if (!Enum.IsDefined(typeof(EClientTypes), client.Type))
        {
            throw new InvalidClientTypeException();
        }
        
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