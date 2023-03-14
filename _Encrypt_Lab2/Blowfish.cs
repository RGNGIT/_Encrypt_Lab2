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
                    // string base64Image = Convert.ToBase64String(image);
                    var header = image[0..54];
                    var list = image.ToList();
                    list.RemoveRange(0, 54);
                    image = list.ToArray();
                    byte[] result = Encrypt(image, sslGenerated ? GlobalKey! : key);
                    result = Program.ArrayJoin(header, result);
                    // Console.WriteLine(Convert.ToBase64String(result));
                    File.WriteAllBytes("blowfish_encrypted_img.bmp", result);
                    break;
                case 2:
                    byte[] toDecrypt = File.ReadAllBytes("blowfish_encrypted_img");
                    string base64Decrypt = Decrypt(toDecrypt, sslGenerated ? GlobalKey! : key);
                    // Console.WriteLine(base64Decrypt);
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
                    byte[] encrypted = Encrypt(toEncrypt, sslGenerated ? GlobalKey! : key);
                    File.WriteAllBytes("blowfish_encrypted", encrypted);
                    Console.WriteLine(toEncrypt + " -> " + Convert.ToBase64String(encrypted));
                    break;
                case 2:
                    byte[] toDecrypt = File.ReadAllBytes("blowfish_encrypted");
                    string decrypted = Decrypt(toDecrypt, sslGenerated ? GlobalKey! : key);
                    Console.WriteLine(Convert.ToBase64String(toDecrypt) + " -> " + decrypted);
                    break;
            }
            Console.ReadKey();
        }
        byte[] Encrypt(byte[] toEncrypt, byte[] key)
        {
            BlowfishCore blowfishCore = new(key);
            byte[] encrypted = blowfishCore.Encipher(toEncrypt, (byte)8);
            return encrypted;
        }
        byte[] Encrypt(string toEncrypt, byte[] key)
        {
            BlowfishCore blowfishCore = new(key);
            byte[] encrypted = blowfishCore.Encipher(ASCIIEncoding.ASCII.GetBytes(toEncrypt), (byte)8);
            return encrypted;
        }
        string Decrypt(byte[] encrypted, byte[] key)
        {
            BlowfishCore blowfishCore = new(key);
            string decrypted = ASCIIEncoding.ASCII.GetString(blowfishCore.Decipher(encrypted, (byte)8));
            return decrypted;
        }
    }
}
