using System.Security.Cryptography;

namespace _Encrypt_Lab2 {
    static class Program
    {

        public static byte[] ArrayJoin(byte[] arr1, byte[] arr2)
        {
            List<byte> list = new ();
            foreach(byte b in arr1) 
            {
                list.Add(b);
            }
            foreach(byte b in arr2)
            {
                list.Add(b);
            }
            return list.ToArray();
        }

        static CipherMode[] modes = new CipherMode[2] 
        {
            CipherMode.CBC,
            CipherMode.ECB,
        }; 

        static AES aes = new(modes[1]);
        static Blowfish blowfish = new();

        static void Main()
        {
            try 
            {
                Console.Clear();
                Console.WriteLine("Выберите метод шифрования\n1 - AES\n2 - Blowjob");
                var selector = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ключ генерит SSL?\n1 - Нет\n2 - Да");
                var selectorSsl = Convert.ToInt32(Console.ReadLine());
                switch (selector)
                {
                    case 1:
                        Console.WriteLine("Что шифруем?\n1 - Строка\n2 - Картинка");
                        selector = Convert.ToInt32(Console.ReadLine());
                        switch(selector)
                        {
                            case 1:
                                aes.AesEncryption(Convert.ToBoolean(selectorSsl - 1));
                                break;
                            case 2:
                                Console.WriteLine("Название картинки");
                                var name = Console.ReadLine();
                                aes.ImageEncryption(name!, Convert.ToBoolean(selectorSsl - 1));
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("Что шифруем?\n1 - Строка\n2 - Картинка");
                        selector = Convert.ToInt32(Console.ReadLine());
                        switch (selector)
                        {
                            case 1:
                                blowfish.DoStuff(Convert.ToBoolean(selectorSsl - 1));
                                break;
                            case 2:
                                Console.WriteLine("Название картинки");
                                var name = Console.ReadLine();
                                blowfish.ImageEncryption(name!, Convert.ToBoolean(selectorSsl - 1));
                                break;
                        }
                        break;
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally 
            {
                Main(); 
            }
        }

    }
}