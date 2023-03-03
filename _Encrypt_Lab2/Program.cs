using System.Text;

namespace _Encrypt_Lab2 {
    static class Program
    {

        static AES aes = new();

        static void Main()
        {
            try 
            {
                Console.Clear();
                Console.WriteLine("Выберите метод шифрования\n1 - AES");
                var selector = Convert.ToInt32(Console.ReadLine());
                switch (selector)
                {
                    case 1:
                        Console.WriteLine("Ключ генерит SSL?\n1 - Нет\n2 - Да");
                        selector = Convert.ToInt32(Console.ReadLine());
                        aes.AesEncryption(Convert.ToBoolean(selector - 1));
                        break;
                    case 2:

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