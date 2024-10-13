using HomeWork13_2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13_2.UI
{
    internal class UI
    {
        /// <summary>
        /// Сообщение меню
        /// </summary>
        static private void messageMenu() => Console.WriteLine("меню:\n1 - добавить книгу;\n2 - вывести список непрочитанного;\n3 - выйти");

        /// <summary>
        /// UI меню программы библиотекарь
        /// </summary>
       async static public void Menu()
        {
            Library library = new Library();
            var cts = new CancellationTokenSource();
            library.AddBooks("Любовь к жизни", 45);
            library.AddBooks("Приключения Мюнхгаузена", 17);
            library.AddBooks("Незнайка в Солнечном городе", 4);
            library.AddBooks("xxx", 1);
            library.AddBooks("xxx удаление", 98);
            library.TaskRunReadBook(cts);
            bool flagStop = false;
            while (!flagStop)
            {
                messageMenu();
                int commandUser;
                if (int.TryParse(Console.ReadLine(), out commandUser))
                {
                    switch (commandUser)
                    {
                        case 1:
                            library.AddBooks(Console.ReadLine(),0);
                            break;
                        case 2:
                            library.GetListBook();
                            break;
                        case 3:
                            cts.Cancel();
                            cts.Dispose();
                            flagStop = true;
                            break;
                        default:
                            break;
                    }
                }

            }
        }
    }
}
