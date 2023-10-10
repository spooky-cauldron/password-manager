using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PangeaCyber.Net;
using PangeaCyber.Net.Redact;
using PangeaCyber.Net.Vault;
using PangeaCyber.Net.Vault.Requests;

namespace PasswordManager.Services;

public class Pangea : ISecurity
{
    ILogger logger;
    VaultClient vaultClient;
    RedactClient redactClient;
    string keyId;
    
    public Pangea(IConfiguration config, ILogger<Pangea> logger)
    {
        this.logger = logger;
        logger.LogInformation("Initializing Pangea service...");
        
        // load pangea configuration
        var pangeaToken = GetConfigValue(config, "Pangea:Token");
        var pangeaDomain = GetConfigValue(config, "Pangea:Domain");
        keyId = GetConfigValue(config, "Pangea:KeyId");

        // initialize pangea clients
        var cfg = new Config(pangeaToken, pangeaDomain);
        vaultClient = new VaultClient.Builder(cfg).Build();
        redactClient = new RedactClient.Builder(cfg).Build();
        
        logger.LogInformation("Initialized Pangea service.");
    }

    public async Task<string> Encrypt(string text)
    {
        var b64Text = Utils.StringToStringB64(text);
        var encryptRequest = new EncryptRequest.Builder(keyId, b64Text).Build();
        var encryptedResponse = await vaultClient.Encrypt(encryptRequest);
        var encrypted = encryptedResponse.Result.CipherText;
        logger.LogDebug($"Encrypted: {encrypted}");
        return encrypted;
    }

    public async Task<string> Decrypt(string ciphertext)
    {
        var decryptRequest = new DecryptRequest.Builder(keyId, ciphertext).Build();
        var decryptResponse = await vaultClient.Decrypt(decryptRequest);
        var decrypted = Utils.Base64ToString(decryptResponse.Result.PlainText);
        return decrypted;
    }

    public async Task<string> Redact(string text)
    {
        var redactRequest = new RedactTextRequest.Builder(text).Build();
        var redactResponse = await redactClient.RedactText(redactRequest);
        var redacted = redactResponse.Result.RedactedText;
        logger.LogDebug($"Redacted result: {redacted}");
        return redacted;
    }

    string GetConfigValue(IConfiguration config, string name)
    {
        var value = config[name];
        if (value == null)
        {
            logger.LogError($"{name} config not found");
            throw new Exception($"{name} config not found");
        }
        return value;
    }
}