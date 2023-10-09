namespace PasswordManager
{
    public class PasswordData
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public PasswordData(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}