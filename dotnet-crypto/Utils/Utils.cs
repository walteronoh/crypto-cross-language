using System.Security.Cryptography;
using System.Text;

class AppUtils
{

    private readonly string key = "abcdefghijklmnopqrstuvwxyz123456";
    public (byte[] ciphertext, byte[] nonce, byte[] tag) Encrypt(String plainText = "{name: 'Walter Kiprono', role: 'Developer'}")
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        var aes = new AesGcm(keyBytes);

        var nonce = new Byte[AesGcm.NonceByteSizes.MaxSize]; // MaxSize = 12, Also known as the iv (Initialization Vector)

        RandomNumberGenerator.Fill(nonce);

        var plaintextBytes = Encoding.UTF8.GetBytes(plainText);

        var ciphertext = new byte[plaintextBytes.Length];

        var tag = new byte[AesGcm.TagByteSizes.MaxSize]; // MaxSize = 16

        aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);

        DecryptWithNet(ciphertext, nonce, tag);

        return (ciphertext, nonce, tag);
    }
    public string Decrypt(String ciphertext, byte[] nonce, byte[] tag)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        var aes = new AesGcm(keyBytes);

        var ciphertextBytes = Encoding.UTF8.GetBytes(ciphertext);

        var plaintextBytes = new Byte[ciphertextBytes.Length];

        aes.Decrypt(nonce, ciphertextBytes, tag, plaintextBytes);

        return Encoding.UTF8.GetString(plaintextBytes);
    }

    private string DecryptWithNet(byte[] ciphertext, byte[] nonce, byte[] tag)
    {
        Console.WriteLine("-------------------------------------------------------------------------------");
        Console.WriteLine(nonce.Length);
        var txt = Encoding.ASCII.GetString(nonce);
        Console.WriteLine(txt);
        var txtBytes = Encoding.ASCII.GetBytes(txt);
        Console.WriteLine(txtBytes.Length);
        Console.WriteLine("-------------------------------------------------------------------------------");
        Console.WriteLine("Original nonce: " + string.Join(", ", nonce));
        Console.WriteLine("Encoded nonce: " + string.Join(", ", txtBytes));

        Console.WriteLine("Original nonce Text: " + string.Join(", ", Encoding.UTF8.GetString(nonce)));
        Console.WriteLine("Encoded nonce Text: " + string.Join(", ", Encoding.UTF8.GetString(txtBytes)));
        using (var aes = new AesGcm(Encoding.UTF8.GetBytes(key)))
        {
            var plaintextBytes = new byte[ciphertext.Length];

            aes.Decrypt(nonce, ciphertext, tag, plaintextBytes);

            Console.WriteLine(Encoding.UTF8.GetString(plaintextBytes));

            return Encoding.UTF8.GetString(plaintextBytes);
        }
    }
}