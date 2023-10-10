namespace PasswordManager;

public class PasswordStore
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string EncryptedValue { get; set; }
}