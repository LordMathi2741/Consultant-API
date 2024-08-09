using Support.Models;

namespace Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<long?> ValidateToken(string token);
}