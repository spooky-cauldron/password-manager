namespace PasswordManager;

public class PasswordInfo
{
    public long Id { get; set; }
    public string Name { get; set; }

    public PasswordInfo(long id, string name)
    {
        Id = id;
        Name = name;
    }
}