using Client = Support.Models.Client;

namespace Security.Interfaces;

public interface ITokenService
{
    string GenerateToken(Client client);
    Task<long?> ValidateToken(string token);
}