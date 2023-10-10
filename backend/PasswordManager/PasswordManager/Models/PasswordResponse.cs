namespace PasswordManager
{
    public class PasswordResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public PasswordResponse(long id, string name, string value)
        {
            Id = id;
            Name = name;
            Value = value;
        }
    }
}