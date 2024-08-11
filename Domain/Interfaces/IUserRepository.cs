using Infrastructure.Interfaces;
using Support.Models;

namespace Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> SignUp(User user);
    
    Task<string> SignIn (string email, string password);
    
    Task<User?> GetClientByEmail(string email);
    Task<User?> UpdateClient(User user);
    
    Task<User?> UpdateClientRole(User user, string role);
    Task DeleteClient(User user);
    
}