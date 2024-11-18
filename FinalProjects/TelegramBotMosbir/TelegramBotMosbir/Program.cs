using MenuProject;
using SettingsProject;

namespace MainProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            start();
        }
        /// <summary>
        /// Старт приложения ловли ошибок
        /// </summary>
        static public void start()
        {
            try
            {
                Settings.CheckFileSetting();
              //  GraphicCreator.DelAllIMGInTemp();
                Menu.start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
