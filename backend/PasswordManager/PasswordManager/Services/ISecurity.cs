using System.Threading.Tasks;

namespace PasswordManager.Services;

public interface ISecurity
{
    public Task<string> Encrypt(string text);
    public Task<string> Decrypt(string cyphertext);
    public Task<string> Redact(string text);
}