using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _09_HomeWork
{

    internal class Program
    {
        static void Main(string[] args)
        {
            task1_3();
            task4(); //4 задание
            task5(); // 5 задание
        }

        #region 1 - 3 задание
        static void task1_3()
        {
            ImageDownLoader image = new ImageDownLoader();
            image.NotifyStart += image.ImageStarted;
            image.NotifyEnd += image.ImageFinished;

            image.Download(
             remoteUri: @"https://a.d-cd.net/wgAAAgPivuA-1920.jpg",
             savePath: "C:\\Users\\Ilya\\Desktop\\folderLoadImg",
             nameFile: "1.jpg"
         );
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
        async static Task task4()
        {
            //вызывается в основном теле main
            bool flagStop = false;
    
            var cts = new CancellationTokenSource();
            // для принудительной остановки потока
            ImageDownLoader image = new ImageDownLoader();

            var s = Task.Run(() => image.Download(
                remoteUri: "https://a.d-cd.net/wgAAAgPivuA-1920.jpg",
                savePath: "C:\\Users\\Ilya\\Desktop\\folderLoadImg\\ansys.jpg"
                ), cts.Token);

            while (!flagStop)
            {
                Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
                string key = Console.ReadLine().ToUpper();
                if (key == "A" || key == "А")
                {//A- анг раскладка;  А -рус раскладка
                    string status = s.IsCompleted ? "загружена" : "нет";
                    Console.WriteLine($"состояние загрузки картинки: {status}, но я всё равно выйду!");
                    flagStop = true;
                    cts.Cancel();
                    cts.Dispose();
                }
                else
                {
                    string status = s.IsCompleted ? "загружена" : "нет";
                    Console.WriteLine($"состояние загрузки картинки: {status}");
                }

            }

        }
        #endregion

        #region 5 задание
        /*
         Модифицируйте программу таким образом, чтобы она скачивала сразу 10 картинок одновременно, останавливая одновременно все потоки 
        по нажатию кнопки "A". По нажатию других кнопок выводить на экран список загружаемых картинок и их состояния - загружены или нет, 
        аналогично выводу для одной картинки.
         */
        //TODO спросить в понедельник почему требуется ставить задержку!
        async static Task task5()
        {
            //вызывается в основном теле main
            bool flagStop = false;
            Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
            var cts = new CancellationTokenSource();
            List<ImageDownLoader> imageList = new List<ImageDownLoader>();
            List<CancellationTokenSource> tocenCansel = new List<CancellationTokenSource>();

            ImageDownLoader imageDownLoader = new ImageDownLoader();
            /// код с циклом
            List<Task> tasks = new List<Task>();
            int countImg = 0;
            for (int i = 0; i < 10; i++)
            {
                int i1 = i;
                // спросить в понедельник почему требуется ставить задержку
                tasks.Add(Task.Run(() => imageDownLoader.Download
                (
                        remoteUri: "https://a.d-cd.net/wgAAAgPivuA-1920.jpg",
                        savePath: $"C:\\Users\\Ilya\\Desktop\\folderLoadImg\\Ansys{i1.ToString()}.jpg"
                ),
                cts.Token));

            }

            while (!flagStop)
            {
                Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
                string key = Console.ReadLine().ToUpper();
                if (key == "A" || key == "А")
                {//A- анг раскладка;  А -рус раскладка

                    cts.Cancel();
                    cts.Dispose();
                    Console.WriteLine($"выход!");
                    flagStop = true;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    countImg = 0;
                    foreach (var task in tasks)
                    {

                        stringBuilder.AppendLine($"IMG {countImg}, status: {task.IsCompleted}");
                        countImg++;

                    }
                    Console.WriteLine($"Скачалась ли картинка: {stringBuilder.ToString()}");
                }
            }

        }


        #endregion

    }
}


