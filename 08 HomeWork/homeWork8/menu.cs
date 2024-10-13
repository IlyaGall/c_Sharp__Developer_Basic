using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork8
{
    /// <summary>
    /// управление запуском программы
    /// </summary>
    internal class Menu
    {

        /// <summary>
        /// проверка команд на корректность
        /// </summary>
        /// <returns>(int) команды</returns>
        static private int commandMenu() 
        {
            Console.WriteLine("Добро пожаловать в программу бинарного дерева.\nЕсть команды 0,1 и -1.\n0 - (переход к началу программы постройка дерева с вводом значений).\n1 (снова поиск зарплаты).\n-1 выход из программы");
            int command =0;
            bool flagConvert = false;
            while (!flagConvert) 
            {
                flagConvert = int.TryParse(Console.ReadLine(), out command);
                if(!flagConvert) { Console.WriteLine("не корректная команда!"); }
            }
            return command;
            
        }

        /// <summary>
        /// проверка введённых данных пользователем
        /// </summary>
        /// <param name="countItems"></param>
        /// <returns>имя,зп</returns>
        static private (string, double) tryConvector(ref int countItems) 
        {
            Console.WriteLine($"{countItems}: ");
            countItems++;
            bool flagConvert = false;
            double number = 0;
            while (!flagConvert)
            {
                string[] data = Console.ReadLine().Split(' ');
                if (data[0] == "0")
                {
                    return ("0", 0);
                }
                if (data.Count() == 2)
                {
                    flagConvert = double.TryParse(data[1], out number);
                    if (!flagConvert)
                    {
                        Console.WriteLine("Это не число!"); 
                    } 
                    else 
                    { 
                        return (data[0],number); 
                    }
                }
                else 
                {
                    Console.WriteLine("Не корректные данные");
                }
            }
            
            return ("", number);
        }

       /// <summary>
       /// обработчик команд
       /// </summary>
       static public void command()
        {
            BinaryTree binaryTree=new BinaryTree();
            
            bool exit = false;
            while (!exit)
            {
                switch (commandMenu())
                {
                    case (0):
                        int  countItems = 1;
                        Console.WriteLine($"Введите корень {countItems} в формате 'Имя ЗП', 0 - означает конец ввода");
                        var array = tryConvector(ref countItems);
                        binaryTree = new BinaryTree(array.Item1, array.Item2);
                        bool flagEndRead = false;
                        while (!flagEndRead)
                        {
                            array = tryConvector(ref countItems);
                            if (array.Item1 != "0")
                            {
                                binaryTree.Add(array.Item1, array.Item2);
                            }
                            else 
                            {
                                binaryTree.symmetrySearch();
                                flagEndRead=true;
                            }
                        }
                        break;
                    case (1):
                        if (binaryTree.value == null)
                        {
                            Console.WriteLine($"Дерево пустое, поиск не возможен!");

                        }
                        else 
                        {
                            double number = 0;
                            bool flagConvert = false;
                            while (!flagConvert)
                            {
                                Console.WriteLine("Введите зп, чтобы найти пользователя.\n");
                                flagConvert = double.TryParse(Console.ReadLine(), out number);

                            }
                            binaryTree.SalarySearch(number);
                        }
                        

                        break;
                    case (-1):
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Не известная команда.\nЕсть команды 0,1 и -1.\n0 - (переход к началу программы постройка дерева с вводом значений).\n1 (снова поиск зарплаты).\n-1 выход из программы");
                        break;
                }
            }
        }

    }
}
