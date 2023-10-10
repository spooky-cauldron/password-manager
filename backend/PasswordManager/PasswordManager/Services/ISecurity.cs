namespace PasswordManager.Services;

public interface ISecurity
{
    public string Encrypt(string text);
    public string Decrypt(string cyphertext);
    public string Redact(string text);
}