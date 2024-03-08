using System.Buffers.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_crypto.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EncryptDecryptController : ControllerBase
{
    private readonly ILogger<EncryptDecryptController> _logger;

    public EncryptDecryptController(ILogger<EncryptDecryptController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    [Route("Encrypt")]
    public Decrypt Encrypt(Encrypt payload)
    {
        AppUtils appUtils = new();
        var (ciphertext, nonce, tag) = appUtils.Encrypt(payload.PlainText);
        Decrypt data = new()
        {
            Iv = nonce,
            EncryptedData = Convert.ToBase64String(ciphertext),
            Tag = tag
        };
        return data;
    }

    [HttpPost]
    [Route("Decrypt")]
    public string Decrypt(Decrypt decrypt)
    {
        AppUtils appUtils = new();
        return appUtils.Decrypt(decrypt.EncryptedData, decrypt.Iv, decrypt.Tag);
    }
}
