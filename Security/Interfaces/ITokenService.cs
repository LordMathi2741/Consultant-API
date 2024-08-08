using Infrastructure.Models;

namespace Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(Client client);
    Task<long?> ValidateToken(string token);
}