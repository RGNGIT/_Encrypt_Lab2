using System.Security.Cryptography;

namespace _Encrypt_Lab2
{
    internal static class SSL
    {
        static public byte[] GenerateKey()
        {
            using(Aes aes = Aes.Create()) 
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }
    }
}
