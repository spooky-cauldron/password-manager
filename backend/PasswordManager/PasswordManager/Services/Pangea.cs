using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PasswordManager.Services;

public class Pangea : ISecurity
{
    string token;
    
    public Pangea(IConfiguration config, ILogger<Pangea> logger)
    {
        logger.LogInformation("Initializing Pangea service...");
        var pangeaToken = config["Pangea:Token"];
        if (pangeaToken == null)
        {
            logger.LogError("pangea token not found");
            throw new Exception("pangea token not found");
        }
        token = pangeaToken;
        logger.LogInformation("Initialized Pangea service.");
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