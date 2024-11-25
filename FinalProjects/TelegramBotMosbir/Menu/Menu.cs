using SettingsProject;
using Server.TelegramBot;

namespace MenuProject
{
    public static class Menu
    {
        #region управление курсором меню
        /// <summary>
        /// количество элементов в меню на текущий момент
        /// </summary>
        static public int AmountElementsInNowMenu = 0;

        /// <summary>
        /// Исходное положение стрелки меню
        /// </summary>
        static public int selectedValue = 0;

        /// <summary>
        /// стек текущей навигации пользователя
        /// </summary>
        static public List<string> stackNavigation = new List<string>() { "MainAnalitcs menu" };

        /// <summary>
        /// На одну строку вниз
        /// </summary>
        private static void SetDown()
        {
            if (selectedValue < AmountElementsInNowMenu)
            {
                selectedValue++;
            }
            else
            {
                selectedValue = 1;
            }
        }

        /// <summary>
        /// На одну строку вверх
        /// </summary>
        private static void SetUp()
        {
            if (selectedValue > 1)
            {
                selectedValue--;
            }
            else
            {
                selectedValue = AmountElementsInNowMenu;
            }
        }
        private static void WriteCursor(int pos)
        {
            Console.SetCursorPosition(0, pos);
            Console.Write(">");
            Console.SetCursorPosition(0, pos);
        }

        private static void ClearCursor(int pos)
        {
            Console.SetCursorPosition(0, pos);
            Console.Write(" ");
            Console.SetCursorPosition(0, pos);
        }
        #endregion

        /// <summary>
        /// Вывести меню
        /// </summary>
        private static void PrintMenu()
        {
            if (stackNavigation.Count > 0)
            {
                switch (stackNavigation[stackNavigation.Count - 1])
                {
                    case "MainAnalitcs menu":
                        Console.WriteLine("Для выхода нажмите Escape");
                        for (var i = 0; i < optionsMainMenu.Length; i++)
                        {
                            Console.WriteLine($" {i + 1}. {optionsMainMenu[i]}");
                        }
                        AmountElementsInNowMenu = optionsMainMenu.Length;
                        break;
                    case "Setting":
                        Console.WriteLine("Для выхода нажмите Escape");
                        for (var i = 0; i < settings.Length; i++)
                        {
                            Console.WriteLine($" {i + 1}. {settings[i]}");

                        }
                        AmountElementsInNowMenu = settings.Length;
                        break;
                    case "Author":
                        Console.WriteLine(" Для выхода нажмите Escape");
                        for (var i = 0; i < author.Length; i++)
                        {
                            Console.WriteLine($" {i + 1}. {author[i]}");

                        }
                        AmountElementsInNowMenu = author.Length;
                        selectedValue = 0;
                        break;
                }
            }

        }

        /// <summary>
        /// Опции меню
        /// </summary>
        private static string[] optionsMainMenu = new[]
        {
            "Запустить сервер",
            "Настройки",
            "Автор",
            "Выход из программы"

        };

        /// <summary>
        /// Обновление раздела настроек 
        /// </summary>
        private static string[] settings = new[] {
            $"Временное хранение файлов графиков '{Settings.GlobalParameters.PathSave}'",
            $"Сбросить файл настроек по умолчанию",
            $"Версия по '{Settings.GlobalParameters.VersionProgram}'",
            $"Ширина картинки = {Settings.GlobalParameters.WithIMG}",
            $"Высота картинки = {Settings.GlobalParameters.HeightIMG}",
            $"Размер интервала свечки = {Settings.GlobalParameters.CandleInterval}",
            $"Токен telegram = *скрыто*",
            "Выход в меню"
        };

        /// <summary>
        /// Обновление массива 
        /// </summary>
        private static void updateArraySetting()
        {
            settings = new[] {
            $"Временное хранение файлов графиков '{Settings.GlobalParameters.PathSave}'",
            $"Сбросить файл настроек по умолчанию",
            $"Версия по '{Settings.GlobalParameters.VersionProgram}'",
            $"Ширина картинки = {Settings.GlobalParameters.WithIMG}",
            $"Высота картинки = {Settings.GlobalParameters.HeightIMG}",
            $"Размер интервала свечки = {Settings.GlobalParameters.CandleInterval}",

             $"Токен telegram = *скрыто*",
                "Выход в меню"
            };

        }

        /// <summary>
        /// Возвращаем фичу
        /// </summary>
        private static string[] author = new[] {
            $"Автор ПО '{Settings.GlobalParameters.Avtar}'",
        };

        /// <summary>
        /// Навигация по меню
        /// </summary>
        /// <param name="movement">В какую строну движение + вперёд</param>
        /// <returns>bool try если дошли до конца пути</returns>
        static private bool navigation(string movement = "+")
        {
            if (movement == "+")
            {//переключение по меню
                switch (selectedValue)
                {
                    case 0:
                        break;
                    case 1:
                        if (stackNavigation[stackNavigation.Count - 1] == "MainAnalitcs menu")
                        {
                            Console.Clear();
                            TelegramBotMessage.startTelegram();

                            Clear();
                            PrintMenu();
                        }
                        if (stackNavigation[stackNavigation.Count - 1] == "Setting")
                        {
                            Console.Clear();
                            Console.WriteLine("Введите новый путь(ПКМ вставить из буфера обмена) если ничего не ввести, то новый путь не будет установлен:");
                            string? s = Console.ReadLine();
                            if (Settings.CorrectPathDirectory(s))
                            {
                                Settings.GlobalParameters.PathSave = s;
                                updateArraySetting();
                                Console.Clear();
                                PrintMenu();
                            }
                            else
                            {
                                Console.Clear();
                                PrintMenu();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Внимание не удалось изменить путь на '{s}' путь сброшен на дефолтный {Settings.GlobalParameters.PathSave} \n{Settings.GlobalParameters.Error}\nЧтобы ещё раз попробовать выберите пункт 1 и нажмите 'enter'");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        break;
                    case 2:
                        switch (stackNavigation[stackNavigation.Count - 1])
                        {
                            case "MainAnalitcs menu":
                                Clear();
                                stackNavigation.Add("Setting");
                                PrintMenu();
                                break;
                            case "Setting":

                                Console.Clear();
                                Console.WriteLine("Вы действительно хотите сбросить все настройки по умолчанию? Введите: 'y/n' или 'д/н'");
                                string? command = Console.ReadLine()?.ToLower();
                                if (command == "y" || command == "д")
                                {
                                    Settings.CheckFileSetting(true);
                                }
                                updateArraySetting();
                                Console.Clear();
                                PrintMenu();
                                break;
                        }
                        break;
                    case 3:
                        Clear();
                        stackNavigation.Add("Author");
                        PrintMenu();
                        break;
                    case 4:
                        Clear();
                        switch (stackNavigation[stackNavigation.Count - 1])
                        {
                            case "MainAnalitcs menu":
                                stackNavigation.RemoveAt(stackNavigation.Count - 1);
                                PrintMenu();
                                selectedValue = 1;
                                return true;
                            case "Setting":
                                Console.Clear();
                                Console.WriteLine("Ведите новую ширину");

                                if (int.TryParse(Console.ReadLine(), out var with))
                                {
                                    Settings.GlobalParameters.WithIMG = with;
                                    updateArraySetting();
                                    Settings.CheckFileSetting(true);
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Внимание не удалось преобразовать {with} в целочисленное число. Сброшено на дефолтные значения.{Settings.GlobalParameters.Error}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                PrintMenu();
                                break;

                            default:
                                return true;
                        }
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Ведите новую высоту");
                        if (int.TryParse(Console.ReadLine(), out var heigh))
                        {
                            Settings.GlobalParameters.HeightIMG = heigh;
                            updateArraySetting();
                            Settings.CheckFileSetting(true);
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Внимание не удалось преобразовать {heigh} в целочисленное число. Сброшено на дефолтные значения.{Settings.GlobalParameters.Error}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        PrintMenu();
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("Ведите новый размер интервала");
                        if (int.TryParse(Console.ReadLine(), out var candleInterval))
                        {
                            Settings.GlobalParameters.CandleInterval = candleInterval.ToString();
                            updateArraySetting();
                            Settings.CheckFileSetting(true);
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Внимание не удалось преобразовать {candleInterval} в целочисленное число. Сброшено на дефолтные значения.{Settings.GlobalParameters.Error}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        PrintMenu();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Ведите token Telegram");

                        Settings.GlobalParameters.Token = Console.ReadLine();
                        updateArraySetting();
                        Settings.CheckFileSetting(true);
                        Console.Clear();
                        PrintMenu();
                        break;
                    case 8:
                        Console.Clear();
                        stackNavigation.RemoveAt(stackNavigation.Count - 1);
                        PrintMenu();
                        selectedValue = 1;
                        return false;
                }
            }
            else
            {
                Clear();
                stackNavigation.RemoveAt(stackNavigation.Count - 1);
                PrintMenu();
            }
            selectedValue = 1;
            return false;
        }

        /// <summary>
        /// Запуск меню
        /// </summary>
        private static void Start()
        {
            ConsoleKeyInfo ki;
            selectedValue = 1;
            AmountElementsInNowMenu = optionsMainMenu.Length;
            PrintMenu();
            WriteCursor(selectedValue);
            bool flagExit = false;
            do
            {
                ki = Console.ReadKey();
                ClearCursor(selectedValue);
                switch (ki.Key)
                {
                    case ConsoleKey.UpArrow:
                        SetUp();
                        break;
                    case ConsoleKey.DownArrow:
                        SetDown();
                        break;
                    case ConsoleKey.W:
                        SetUp();
                        break;
                    case ConsoleKey.S:
                        SetDown();
                        break;
                    #region цифры на клавиатуре
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                        selectedValue = int.Parse(ki.KeyChar.ToString());
                        break;
                    #endregion

                    case ConsoleKey.Enter:
                        flagExit = navigation();
                        break;
                    case ConsoleKey.Escape:
                        navigation("-");
                        if (stackNavigation.Count == 0)
                        {
                            flagExit = true;
                        }
                        break;
                }
                WriteCursor(selectedValue);
            } while (!flagExit);

        }

        /// <summary>
        /// Очистка консоли 
        /// </summary>
        private static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Запуск приложения
        /// </summary>
        static public void start()
        {
            Start();
        }
    }
}
