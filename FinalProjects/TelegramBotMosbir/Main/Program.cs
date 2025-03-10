﻿
using SettingsProject;

namespace Main
{
    internal class Program
    {
        
            /// <summary>
            /// Старт приложения
            /// </summary>
            /// <param name="args"></param>
            static void Main(string[] args)
            {
                start();
            }

            /// <summary>
            /// Старт приложения ловли ошибок
            /// </summary>
            static public void start()
            {

                Settings.CheckFileSetting();
                Menu.start();
                DelAllIMGInTemp();
            }

            /// <summary>
            /// Удаление всех файлов после завершения работы
            /// </summary>
            static public void DelAllIMGInTemp()
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Settings.GlobalParameters.PathSave);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }
        
    }
}
