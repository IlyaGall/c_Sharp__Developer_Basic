using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
namespace homeWork7
{
    /*
    Реализовать метод нахождения n-го члена последовательности Фибоначчи по формуле F(n) = F(n-1) + F(n-2) с помощью рекурсивных вызовов.
    Реализовать метод нахождения n-го члена последовательности Фибоначчи по формуле F(n) = F(n-1) + F(n-2) с помощью цикла.
    Добавить подсчёт времени на выполнение рекурсивного и итеративного методов с помощью Stopwatch и написать сколько времени для значений 5, 10 и 20.
     */

    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch clock = new Stopwatch();
            int[] array = new int[3] { 5, 10, 20 };
            for (int i = 0; i < 100; i++)
            {

            }
            Console.WriteLine("Для рекурсии");
            foreach (int oneElement in array)
            {
                clock.Start();
                var s = getRecurs(oneElement);
                clock.Stop();
                Console.WriteLine($"затраченное время для {oneElement}: {clock.ElapsedMilliseconds} {clock.ElapsedTicks}");

            }
            Console.WriteLine("Для цикла");
            clock.Reset();
            foreach (int oneElement in array)
            {
                clock.Start();
                var notR = getNotRecurs(oneElement);
                clock.Stop();
                Console.WriteLine($"затраченное время для {oneElement}: {clock.ElapsedMilliseconds} {clock.ElapsedTicks}");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Фибоначчи через рекульсия
        /// </summary>
        /// <param name="n">число Фибоначчи, которое нужно вычислить</param>
        /// <returns>найдено число</returns>
        static int getRecurs(int n)
        {
            if (n <= 2)
            {
                return 1;
            }
            else
            {
                int u = getRecurs(n - 1) + getRecurs(n - 2);
                return u;
            }
        }

        /// <summary>
        /// Фибоначчи через цикл
        /// </summary>
        /// <param name="n">число Фибоначчи, которое нужно вычислить</param>
        /// <returns>найдено число</returns>
        static int getNotRecurs(int n)
        {
            if (n <= 2)
            {
                return 1;
            }
            int sum_1 = 1;
            int sum_2 = 1;
            int sumResult = 0;
            for (int i = 1; i < n - 1; i++)
            {
                sumResult = sum_1 + sum_2;
                sum_1 = sum_2;
                sum_2 = sumResult;
            }
            return sumResult;
        }
    }
}