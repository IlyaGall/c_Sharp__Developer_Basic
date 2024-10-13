namespace homeWork10
{
    internal class Program
    {
        /*
         Создать директории c:\Otus\TestDir1 и c:\Otus\TestDir2 с помощью класса DirectoryInfo.
        В каждой директории создать несколько файлов File1...File10 с помощью класса File.
        В каждый файл записать его имя в кодировке UTF8. Учесть, что файл может быть удален, либо отсутствовать права на запись.
        Каждый файл дополнить текущей датой (значение DateTime.Now) любыми способами: синхронно и\или асинхронно.
        Прочитать все файлы и вывести на консоль: имя_файла: текст + дополнение.
         */
        static void Main(string[] args)
        {
            
            string path1 = @"C:\Otus\TestDir1";
            string path2 = @"C:\Otus\TestDir2";

            Directory.CreateDirectory(path1);
            Directory.CreateDirectory(path2);
            List<string> listPathFiles = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                
                 var file = Directory.WriteInFile(
                   path: Directory.CreateFile(
                            path: path1,
                            nameFile: $"File{i}"),
                   text: $"File{i}");
                listPathFiles.Add(file.Item2);
                if (file.Item1) 
                {
                    Directory.WriteInFile(file.Item2, DateTime.Now.ToString("dd.MM.yyyy"));
                }
            }
            for (int i = 0; i < 10; i++)
            {
                var file = Directory.WriteInFile(
                   path:Directory.CreateFile(
                            path: path2,
                            nameFile: $"File{i}"),
                   text:$"File{i}");
                listPathFiles.Add(file.Item2);
                if (file.Item1)
                {
                    Directory.WriteInFile(file.Item2, DateTime.Now.ToString("dd.MM.yyyy"));
                }
            }

            foreach (var file in listPathFiles)
            {
                //Прочитать все файлы и вывести на консоль: имя_файла: текст + дополнение.
                Console.WriteLine($"Название файла {file}, содержимое файла {Directory.OpenAndReadFile(file)}");
            } 
            Console.WriteLine("Для завершения и выхода из консоли нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
