using System.Reflection.Metadata;

namespace _03_homeWork
{

    enum Severity
    {
        /// <summary>
        /// Вещественных значений не найдено
        /// </summary>
        Warning,
        /// <summary>
        /// параметр(ы) не прошли Парсинг
        /// </summary>
        Error,
        /// <summary>
        /// переполнение 
        /// </summary>
        Overflow

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("a * x^2 + b * x + c = 0");

            writeFromConsole();


        }


        static void calculation(int a, int b, int c)
        {

            double D = b * b - 4 * a * c;

            switch (D)
            {
                case > 0:
                    Console.WriteLine($"x1= {(b * (-1) + Math.Sqrt(D)) / 2 * a}");
                    Console.WriteLine($"x2= {(b * (-1) - Math.Sqrt(D)) / 2 * a}");
                    break;

                case >= 0:
                    Console.WriteLine($"x1= {(b * (-1) + Math.Sqrt(D)) / 2 * a}");
                    break;

                case < 0:

                    try
                    {
                        throw new Exception("Дискриминант меньше нуля");
                    }
                    catch
                    {
                        FormatData(
                            message: "Дискриминант меньше нуля",
                            severity: Severity.Warning,
                            parameter: "");


                    }

                    break;
            }
            if (D > 0)
            {

            }
        }


        static void writeFromConsole()
        {

            bool flagConvert = true;
            int[] arr = new int[3];
            do
            {
                flagConvert = true;
                string errorLogFormatException = "";
                string errorLogOverflowException = "";
                string a = read("a");
                string b = read("b");
                string c = read("c");
                try
                {
                    arr[0] = Convert.ToInt32(a);
                }
                catch (Exception e)
                {

                    if (e is FormatException)
                    {
                        errorLogFormatException = FormatData(errorLogFormatException, Severity.Error, "a");
                        flagConvert = false;
                    }
                    if (e is OverflowException)
                    {
                        errorLogOverflowException = FormatData(errorLogOverflowException, Severity.Overflow, "a");
                        flagConvert = false;
                    }

                }
                try
                {
                    arr[1] = Convert.ToInt32(b);
                }
                catch (Exception e)
                {
                    if (e is OverflowException)
                    {
                        errorLogOverflowException = FormatData(errorLogOverflowException, Severity.Overflow, "b");
                        flagConvert = false;

                    }

                    if (e is FormatException)
                    {
                        errorLogFormatException = FormatData(errorLogFormatException, Severity.Error, "b");
                        flagConvert = false;
                    }

                }

                try
                {
                    arr[2] = Convert.ToInt32(c);
                }
                catch (Exception e)
                {
                    if (e is OverflowException)
                    {
                        errorLogOverflowException = FormatData(errorLogOverflowException, Severity.Overflow, "c");
                        flagConvert = false;
                    }
                    if (e is FormatException)
                    {
                        errorLogFormatException = FormatData(errorLogFormatException, Severity.Error, "c");
                        flagConvert = false;
                    }

                }
                if (!flagConvert)
                {
                    if (errorLogFormatException.Length > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine(errorLogFormatException);
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine($"a = {a}");
                        Console.WriteLine($"b = {b}");
                        Console.WriteLine($"c = {c}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    if (errorLogOverflowException.Length > 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine(errorLogOverflowException);
                        Console.WriteLine("--------------------------------");
                        Console.WriteLine($"a = {a}");
                        Console.WriteLine($"b = {b}");
                        Console.WriteLine($"c = {c}");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                }
            } while (!flagConvert);
            calculation(arr[0], arr[1], arr[2]);


        }






        /// <summary>
        /// Обрабатывает ошибку при конвертации параметра и записывает лог
        /// </summary>
        /// <param name="message">файл лог</param>
        /// <param name="severity">перечисление Severity</param>
        /// <param name="parameter">имя параметра</param>
        /// <returns>лог ошибки</returns>
        static string FormatData(string message, Severity severity, string parameter = "")
        {
            switch (severity)
            {

                case Severity.Error:
                    if (message.Length == 0)
                    {
                        return $"Не верный формат параметра {parameter}";
                    }
                    else
                    {
                        return $"{message.Replace("параметра", "параметров")}, {parameter}";
                    }
                case Severity.Warning:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Дискриминант меньше нуля");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    return "";
                case Severity.Overflow:
                    if (message.Length == 0)
                    {
                        return $"Нужно вводить значения От −2 147 483 648 до 2 147 483 647. Параметр: {parameter}";
                    }
                    else
                    {
                        return $"{message.Replace("параметр", "параметры")}, {parameter}";
                    }
                default:
                    return "";
            }


        }



        /// <summary>
        /// считывание параметров
        /// </summary>
        /// <param name="nameParameter">имя параметра для отображения в консоли</param>
        /// <returns></returns>
        static string read(string nameParameter)
        {
            Console.WriteLine($"Введите значение {nameParameter}:");
            return Console.ReadLine();
        }


    }
}
