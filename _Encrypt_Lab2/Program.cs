namespace _Encrypt_Lab2 {
    static class Program
    {

        static AES aes = new();
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