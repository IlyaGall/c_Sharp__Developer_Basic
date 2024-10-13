using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _09_HomeWork
{
    internal class ImageDownLoader
    {
        #region 1 - 3 задание
        /*
         Добавьте события: в классе ImageDownloader в начале скачивания картинки и в конце скачивания картинки выкидывайте события (event) 
         ImageStarted и ImageCompleted соответственно.
         В основном коде программы подпишитесь на эти события, а в обработчиках их срабатываний выводите соответствующие уведомления в консоль: 
         "Скачивание файла началось" и "Скачивание файла закончилось".
         */
        public delegate void AccountHandler();
        public event AccountHandler? NotifyStart;

        public delegate void AccountHandler2();
        public event AccountHandler2? NotifyEnd;

        /// <summary>
        /// загрузка картинки
        /// </summary>
        /// <param name="remoteUri">URL адрес, где расположена картинка</param>
        /// <param name="savePath">Место сохраннее картинки на ПК</param>
        public void Download(string remoteUri, string savePath, string nameFile)
        {
            string fileNameLoad = savePath + "\\" + nameFile;

            using (var myWebClient = new WebClient())
            {
                // Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileNameLoad, remoteUri);
                NotifyStart();
                myWebClient.DownloadFile(remoteUri, fileNameLoad);
                NotifyEnd();
                //Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", fileNameLoad, remoteUri);
            }

        }
        public void ImageStarted()
        {
            Console.WriteLine("Скачивание файла началось");
        }

        public void ImageFinished()
        {
            Console.WriteLine("Скачивание файла закончилось");

        }
        #endregion

        #region 4 задание
        /*
         Сделайте метод ImageDownloader.Download асинхронным. Если Вы скачивали картинку с использованием WebClient.DownloadFile,
         то используйте теперь WebClient.DownloadFileTaskAsync - он возвращает Task.
        В конце работы программы выводите теперь текст "Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания" 
        и ожидайте нажатия любой клавиши. Если нажата клавиша "A" - выходите из программы. В противном случае выводите состояние загрузки картинки 
        (True - загружена, False - нет). Проверить состояние можно через вызов Task.IsCompleted.
         */
        public async Task Download(string remoteUri, string savePath)
        {
            Thread.Sleep(3000);
            // не нашёл картинку большого размера будет имитация большой картинки
            using (var myWebClient = new WebClient())
            {
                await myWebClient.DownloadFileTaskAsync(remoteUri, savePath);
            }
            //  return Task;
        }
        #endregion



    }
}
