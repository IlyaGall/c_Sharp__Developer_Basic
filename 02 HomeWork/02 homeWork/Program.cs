using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace _02_homeWork
{

 

    /*
     * Цель:
Сделать сравнение по скорости работы List, ArrayList и LinkedList.


* Описание/Пошаговая инструкция выполнения домашнего задания:
    * Создать коллекции List, ArrayList и LinkedList.
    * С помощью цикла for добавить в каждую 1 000 000 случайных значений с помощью класса Random.
    * С помощью Stopwatch.Start() и Stopwatch.Stop() замерить длительность заполнения каждой коллекции и вывести значения на экран.
    * Найти 496753-ий элемент, замерить длительность этого поиска и вывести на экран.
    * Вывести на экран каждый элемент коллекции, который без остатка делится на 777. Вывести длительность этой операции для каждой коллекции.
    * Укажите сколько времени вам понадобилось, чтобы выполнить это задание.
     */
    internal class Program
    {

        /// <summary>
        /// преобразование времени выполнения операции в вид понятный вид
        /// </summary>
        /// <param name="nameTast">имя задачи</param>
        /// <param name="stopWatch">экзепляр класса Stopwatch</param>
        /// <returns>форматированное время</returns>
        static private string timeFormat(string nameTast, Stopwatch stopWatch) 
        {
            TimeSpan ts = stopWatch.Elapsed;
            // Создаем строку, содержащую время выполнения операции.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.Write("\nзадача: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{nameTast}:\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Форматированное время: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{elapsedTime}");
            Console.ForegroundColor = ConsoleColor.White;
            return elapsedTime;
           
        }

        static void Main(string[] args)
        {
            Dictionary <string,string> dict = new Dictionary<string,string>();
            string task1 =  list();
            
            Console.WriteLine("\n\n############2 вид коллекции ######################");
            string task2 = arrayList();

            Console.WriteLine("\n\n############3 вид коллекции ######################");
            string task3 = linkedList();

            Console.WriteLine("\n\nИтоговая табличка" +

                $"\n№ задания:       1           2           3\n" +
                $"list       {task1}\n" +
                $"arrayList  {task2}\n" +
                $"linkedList {task3}\n");
            Console.WriteLine("затраченное время: 30 минут на выполнение дз");
            Console.ReadKey();

        }

        /// <summary>
        /// задания для List<int>
        /// </summary>
        /// <returns>время выполения 3-х заданий</returns>
        static string list() 
        {
            string timer = "";
            List<int> myList = new List<int>();
            Random myRandom = new Random();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i <= 1000000; i++)
            {
                myList.Add(myRandom.Next(1, 10000));
            }
            stopWatch.Stop();
            timer += " " + timeFormat("Наполенние массива List<int>", stopWatch);
            
            stopWatch.Start();

            Console.WriteLine($"\nэлемент 496753 коллекции: {myList[496753]}");
            stopWatch.Stop();
            timer += " " + timeFormat("вывод  496753 в List<int>", stopWatch);

            stopWatch.Start();
            Console.WriteLine();
            //777

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЭлементы, которые без остатка делится на 777 ");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < myList.Count; i++)
            {
                if (myList[i] % 777 == 0) 
                {
                    Console.Write($"{myList[i]} ");
                }
            }
            stopWatch.Stop();
            timer +=" " + timeFormat("вывод элемент коллекции, который без остатка делится на 777 в List<int>", stopWatch);
            return timer;
        }

        /// <summary>
        /// задания для ArrayList
        /// </summary>
        /// <returns>время выполения 3-х заданий</returns>
        static string arrayList() 
        {
            string timer = "";
            ArrayList myArraylist = new ArrayList();
            Random myRandom = new Random();
            Stopwatch stopWatch = new Stopwatch();
           
            // 1 задание
            stopWatch.Start();
            for (int i = 0; i <= 1000000; i++)
            {
                myArraylist.Add(myRandom.Next(1, 10000));
            }
            stopWatch.Stop();
            timer+= " "+timeFormat("Наполенние массива ArrayList", stopWatch);
           
            // 2 задание
            stopWatch.Start();
            Console.WriteLine($"\nэлемент 496753 коллекции: {myArraylist[496753]}");
            stopWatch.Stop();
            timer += " " + timeFormat("вывод  496753 в Arraylist", stopWatch);
          
            // 3 задание
            stopWatch.Start();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЭлементы, которые без остатка делится на 777 ");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < myArraylist.Count; i++)
            {
                if (Convert.ToInt16( myArraylist[i]) % 777 == 0)
                {
                    Console.Write($"{myArraylist[i]} ");
                }
            }
            stopWatch.Stop();
            timer += " " + timeFormat("вывод элемент коллекции, который без остатка делится на 777 в Arraylist", stopWatch);
            return timer;
        }
        /// <summary>
        /// задания для linkedList
        /// </summary>
        /// <returns>время выполения 3-х заданий</returns>
        static string linkedList()
        {
            string timer = "";
            LinkedList<int> myLinkedList = new LinkedList<int>();
            Random myRandom = new Random();
            Stopwatch stopWatch = new Stopwatch();
            // 1 задание
            stopWatch.Start();
            for (int i = 0; i <= 1000000; i++)
            {
                myLinkedList.AddLast(myRandom.Next(1, 10000));
            }
            stopWatch.Stop();
            timer += " " + timeFormat("Наполенние массива LinkedList", stopWatch);
           
            // 2 задание
            stopWatch.Start();
            Console.WriteLine($"\nэлемент 496753 коллекции: {myLinkedList.ElementAt(496753)}");
            stopWatch.Stop();
            timer += " " + timeFormat("вывод  496753 в LinkedList", stopWatch);
            stopWatch.Start();
   
            // 3 задание 
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЭлементы, которые без остатка делится на 777 ");
            Console.ForegroundColor = ConsoleColor.White;
            var currentNode = myLinkedList.First;
            if(currentNode != null) 
            {
                if (Convert.ToInt16(currentNode.Value) % 777 == 0)
                {
                   
                }
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                    if (Convert.ToInt16(currentNode.Value) % 777 == 0)
                    {
                        Console.Write($"{currentNode.Value} ");
                    }

                }
            }
           
            stopWatch.Stop();
            timer += " " + timeFormat("вывод элемент коллекции, который без остатка делится на 777 в LinkedList", stopWatch);
            return timer;
        }
    }
}
