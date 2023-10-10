using System;

namespace PasswordManager.Services;

public class Pangea : ISecurity
{
    public Pangea()
    {
        Console.WriteLine("Created Pangea service");
    }

    public string Encrypt(string text)
    {
        return text + " encrypted";
    }

    public string Decrypt(string cyphertext)
    {
        return cyphertext + " decrypted";
    }

    public string Redact(string text)
    {
        return text;
    }
}