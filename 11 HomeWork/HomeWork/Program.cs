namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var otusDictionary = new OtusDictionary();
            for(int i = 0; i < 33; i++) 
            {
                Console.WriteLine($"{i % 32}");
                otusDictionary.Add(i, $"{i} какой-то текст {i}");
            }       
            otusDictionary.Add(40, "test12xxxxxx");
            otusDictionary.Add(65, "test12xxxxxx");
            var getValue = otusDictionary.Get(3);
            var getIndex = otusDictionary[1];
            try
            {
                getValue = otusDictionary.Get(1111);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Ошибка {ex.Message.ToString()}");
            }

            try
            {
                getValue = otusDictionary[111111111];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка {ex.Message.ToString()}");
            }
            Console.ReadKey();
        }
    }
}
