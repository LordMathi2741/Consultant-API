namespace Security.Interfaces;

public interface IEncryptService
{
    string Encrypt(string password);
    bool Verify(string password, string hash);
}