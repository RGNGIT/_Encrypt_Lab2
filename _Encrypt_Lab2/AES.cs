using System.Security.Cryptography;

namespace _Encrypt_Lab2
{
    internal class AES
    {

        byte[]? GlobalKey = null;
        CipherMode mode;

        public AES(CipherMode mode)
        {
            this.mode = mode;
        }

        public void ImageEncryption(string name, bool sslGenerated)
        {
            Console.Clear();
            Console.WriteLine("1 - Зашифровать\n2 - Расшифровать");
            byte[] key = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
            if (sslGenerated)
            {
                // Сгенерированный симметричный ключ
                if (GlobalKey == null) GlobalKey = SSL.GenerateKey();
                Console.WriteLine("Ключ: " + Convert.ToBase64String(GlobalKey));
            }
            var selector = Convert.ToInt32(Console.ReadLine());
            switch(selector)
            {
                case 1:
                    byte[] image = File.ReadAllBytes(name);
                    var header = image[0..54];
                    var list = image.ToList();
                    list.RemoveRange(0, 54);
                    image = list.ToArray();
                    byte[] result = Encrypt(Convert.ToBase64String(image), sslGenerated ? GlobalKey! : key);
                    result = Program.ArrayJoin(header, result);
                    // Console.WriteLine(Convert.ToBase64String(result));
                    File.WriteAllBytes("aes_encrypted_img.bmp", result);
                    break;
                case 2:
                    byte[] toDecrypt = File.ReadAllBytes("aes_encrypted_img.bmp");
                    var headerDecrypt = toDecrypt[0..54];
                    var listDecrypt = toDecrypt.ToList();
                    listDecrypt.RemoveRange(0, 54);
                    toDecrypt = listDecrypt.ToArray();
                    string base64Decrypt = Decrypt(toDecrypt, sslGenerated ? GlobalKey! : key);
                    // Console.WriteLine(base64Decrypt);
                    var resultD = Program.ArrayJoin(headerDecrypt, Convert.FromBase64String(base64Decrypt));
                    File.WriteAllBytes(name, resultD);
                    break;
            }
            Console.ReadKey();
        }

        public void AesEncryption(bool sslGenerated)
        {
            Console.Clear();
            Console.WriteLine("1 - Зашифровать\n2 - Расшифровать");
            var selector = Convert.ToInt32(Console.ReadLine());
            // Просто рандомный base64 ключ
            byte[] key = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
            if (sslGenerated)
            {
                // Сгенерированный симметричный ключ
                if (GlobalKey == null) GlobalKey = SSL.GenerateKey();
                Console.WriteLine("Ключ: " + Convert.ToBase64String(GlobalKey));
            }
            switch (selector)
            {
                case 1:
                    Console.WriteLine("Введите строку для шифрования");
                    string toEncrypt = Console.ReadLine()!;
                    byte[] result = Encrypt(toEncrypt, sslGenerated ? GlobalKey! : key);
                    File.WriteAllBytes("aes_encrypted", result);
                    Console.WriteLine(toEncrypt + " -> " + Convert.ToBase64String(result));
                    break;
                case 2:
                    byte[] toDecrypt = File.ReadAllBytes("aes_encrypted");
                    Console.WriteLine("Результат");
                    Console.WriteLine(Convert.ToBase64String(toDecrypt) + " -> " + Decrypt(toDecrypt, sslGenerated ? GlobalKey! : key));
                    break;
            }
            Console.ReadKey();
        }

        byte[] Encrypt(string toEncrypt, byte[] key)
        {
            byte[] encrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                aes.Mode = mode;
                aes.Padding = PaddingMode.PKCS7;
                using (MemoryStream msEncrypt = new ())
                {
                    msEncrypt.Write(aes.IV, 0, aes.IV.Length);
                    ICryptoTransform encoder = aes.CreateEncryptor();
                    using (CryptoStream csEncrypt = new (msEncrypt, encoder, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new (csEncrypt))
                    {
                        swEncrypt.Write(toEncrypt);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        string Decrypt(byte[] encrypted, byte[] key)
        {
            string decrypted;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = mode;
                aes.Padding = PaddingMode.PKCS7;
                using (MemoryStream msDecryptor = new (encrypted))
                {
                    byte[] readIV = new byte[16];
                    msDecryptor.Read(readIV, 0, 16);
                    aes.IV = readIV;
                    ICryptoTransform decoder = aes.CreateDecryptor();
                    using (CryptoStream csDecryptor = new (msDecryptor, decoder, CryptoStreamMode.Read))
                    using (StreamReader srReader = new (csDecryptor))
                    {
                        decrypted = srReader.ReadToEnd();
                    }
                }
            }
            return decrypted;
        }
    }
}
