namespace dotnet_crypto
{
    public class Encrypt
    {
        public required string PlainText { get; set; }
    }

    public class Decrypt
    {
        public required byte[] Iv { get; set; }
        public required string EncryptedData { get; set; }
        public required byte[] Tag { get; set; }
    }

}