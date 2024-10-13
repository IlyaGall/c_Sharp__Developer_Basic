using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork10
{
    internal class Directory
    {
        /// <summary>
        /// создать папку
        /// </summary>
        /// <param name="path">путь до папки</param>
        public static void CreateDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            directoryInfo.Create();
            {
                Console.WriteLine($"Папка создана по пути: {path}");
            }
        }
        /// <summary>
        /// создать файл
        /// </summary>
        /// <param name="path">путь где должен лежать файл</param>
        /// <param name="nameFile">название файла</param>
        /// <param name="extension">расширение</param>
        /// <returns>путь до файла</returns>
        public static string CreateFile(string path, string nameFile, string extension = ".txt")
        {
            string pathFile = $@"{path}\{nameFile}{extension}";
            using (FileStream fs = File.Create(pathFile)){ };
            return pathFile;
        }

        /// <summary>
        /// записать в файл текст
        /// </summary>
        /// <param name="path">путь до файла</param>
        /// <param name="text">текст, который нужно записать</param>
        /// <returns>item[0]: удалось ли создать файл, item[1]:путь до файла</returns>
        public static (bool, string) WriteInFile(string path, string text)
        {
            try
            {
                if (File.Exists(path))
                {// В каждый файл записать его имя в кодировке UTF8. Учесть, что файл может быть удален, либо отсутствовать права на запись.
                    File.AppendAllText(
                        path: path,
                        contents: ("\n" + text),
                        encoding: Encoding.UTF8
                        );
                    return (true, path);
                }
                else 
                {
                    Console.WriteLine("Файл не может быть открыт, так как его нет по этому пути.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Файл не может быть открыт, так как он используется другим процессом.\nПодробности:\n {ex.ToString()}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Не получись выполнить запись в файл!\nПодробности:\n {ex.ToString()}");
            }
            return (false, "");
        }
        /// <summary>
        /// открыть и прочитать содержимое файла
        /// </summary>
        /// <param name="path">путь до файла</param>
        public static string OpenAndReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);

            }
            else
            {
                throw new Exception("Не получилось записать данные в файл");
            }
          
        }

    }
}
