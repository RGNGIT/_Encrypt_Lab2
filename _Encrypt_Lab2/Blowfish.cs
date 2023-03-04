using Simias.Encryption;
using System.Text;

namespace _Encrypt_Lab2
{
    internal class Blowfish
    {

        byte[]? GlobalKey = null;

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
                    string base64Image = Convert.ToBase64String(image);
                    byte[] result = Encrypt(base64Image, sslGenerated ? GlobalKey! : key);
                    Console.WriteLine(Convert.ToBase64String(result));
                    File.WriteAllBytes("aes_encrypted_img", result);
                    break;
                case 2:
                    byte[] toDecrypt = File.ReadAllBytes("aes_encrypted_img");
                    string base64Decrypt = Decrypt(toDecrypt, sslGenerated ? GlobalKey! : key);
                    Console.WriteLine(base64Decrypt);
                    File.WriteAllBytes(name, Convert.FromBase64String(base64Decrypt));
                    break;
            }
            Console.ReadKey();
        }

        public void DoStuff(bool sslGenerated)
        {
            Console.Clear();
            byte[] key = Convert.FromBase64String("AAECAwQFBgcICQoLDA0ODw==");
            Console.WriteLine("1 - Зашифровать\n2 - Расшифровать");
            var selector = Convert.ToInt32(Console.ReadLine());
            switch(selector)
            {
                case 1:
                    if (sslGenerated)
                    {
                        if (GlobalKey == null) GlobalKey = SSL.GenerateKey();
                        Console.WriteLine("Ключ: " + Convert.ToBase64String(GlobalKey));
                    }
                    Console.WriteLine("Введите строку для шифрования");
                    string toEncrypt = Console.ReadLine()!;
                    File.WriteAllBytes("blowfish_encrypted", Encrypt(toEncrypt, sslGenerated ? GlobalKey! : key));
                    break;
                case 2:
                    byte[] encrypted = File.ReadAllBytes("blowfish_encrypted");
                    Decrypt(encrypted, sslGenerated ? GlobalKey! : key);
                    break;
            }
            Console.ReadKey();
        }
        byte[] Encrypt(string toEncrypt, byte[] key)
        {
            BlowfishCore blowfishCore = new(key);
            byte[] encrypted = blowfishCore.Encipher(ASCIIEncoding.ASCII.GetBytes(toEncrypt), (byte)8);
            Console.WriteLine(toEncrypt + " -> " + Convert.ToBase64String(encrypted));
            return encrypted;
        }
        string Decrypt(byte[] encrypted, byte[] key)
        {
            BlowfishCore blowfishCore = new(key);
            string decrypted = ASCIIEncoding.ASCII.GetString(blowfishCore.Decipher(encrypted, (byte)8));
            Console.WriteLine(Convert.ToBase64String(encrypted) + " -> " + decrypted);
            return decrypted;
        }
    }
}
