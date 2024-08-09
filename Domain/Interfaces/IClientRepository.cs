using Infrastructure.Interfaces;
using Client = Support.Models.Client;

namespace Domain.Interfaces;

public interface IClientRepository : IBaseRepository<Client>
{
    Task<Client> SignUp(Client client);
    
    Task<string> SignIn (string email, string password);
    Task<Client?> UpdateClient(Client client);
    
    Task<Client?> UpdateClientRole(Client client, string role);
    Task DeleteClient(Client client);
    
}