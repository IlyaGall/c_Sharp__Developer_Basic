using ObjectsBot;
using ScottPlot;
using SettingsProject;


namespace GraphicCreator
{
    internal class Creat
    {

        /// <summary>
        /// удаление всех файлов после завершения работы
        /// </summary>
        static public void DelAllIMGInTemp()
        {
            
            DirectoryInfo dirInfo = new DirectoryInfo(Settings.GlobalParameters.PathSave);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }


        /// <summary>
        /// построение графика Время - x, диапазон чисел y
        /// </summary>
        /// <param name="directory">пусть сохранения картинки</param>
        /// <param name="nameFile">название файла</param>
        /// <param name="date">массив дат</param>
        /// <param name="value">массив значений</param>
        static public string GraphicCreatorDateTime(string nameFile, DateTime[] date, double[] value)
        {
            ScottPlot.Plot myPlot = new();
            myPlot.Add.Scatter(date, value); // x , y
            myPlot.Axes.DateTimeTicksBottom();

            myPlot.Axes.Right.MinimumSize = 50;

            Console.WriteLine($"{myPlot.Axes.Bottom.Min} - {myPlot.Axes.Bottom.Max}");

            double startPoint = myPlot.Axes.Bottom.Min;
            double endPoint = myPlot.Axes.Bottom.Max;

            var axLine3 = myPlot.Add.Line(startPoint, 3019, endPoint, 3019);
            var axLine2 = myPlot.Add.Line(startPoint, 3074, endPoint, 3074);
            var axLine1 = myPlot.Add.Line(startPoint, 3132, endPoint, 3132);
            var axLine4 = myPlot.Add.Line(startPoint, 3240, endPoint, 3240);

            axLine1.Color = Colors.Green;
            axLine2.Color = Colors.Green;
            axLine3.Color = Colors.Green;
            axLine4.Color = Colors.Green;


            // SearchForExtremum(value);
            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);

            return $"{Settings.GlobalParameters.PathSave}\\{nameFile}";
        }

        static public string GraphicCreatorDateTimeEMA(string nameFile, DateTime[] date, double[] value, double[] EMA)
        {
            ScottPlot.Plot myPlot = new();
            myPlot.Add.Scatter(date, value); // x , y
            myPlot.Axes.DateTimeTicksBottom();

            myPlot.Axes.Right.MinimumSize = 50;

            Console.WriteLine($"{myPlot.Axes.Bottom.Min} - {myPlot.Axes.Bottom.Max}");


            myPlot.Add.Scatter(date, EMA); // x , y
            myPlot.Axes.DateTimeTicksBottom();


            //double startPoint = myPlot.Axes.Bottom.Min;
            //double endPoint = myPlot.Axes.Bottom.Max;

            //var axLine3 = myPlot.Add.Line(startPoint, 3019, endPoint, 3019);
            //var axLine2 = myPlot.Add.Line(startPoint, 3074, endPoint, 3074);
            //var axLine1 = myPlot.Add.Line(startPoint, 3132, endPoint, 3132);
            //var axLine4 = myPlot.Add.Line(startPoint, 3240, endPoint, 3240);

            //axLine1.Color = Colors.Green;
            //axLine2.Color = Colors.Green;
            //axLine3.Color = Colors.Green;
            //axLine4.Color = Colors.Green;


            // SearchForExtremum(value);
            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);

            return $"{Settings.GlobalParameters.PathSave}\\{nameFile}";
        }




        static public string GraphicCreatorLineSupport(List<Candle> candles, List<LinePoint> linePoints, string nameFile = "за день свечки.png")
        {
            ScottPlot.Plot myPlot = new();
            DateTime timeOpen = candles[0].Begin;// new(2024, 01, 03, 9, 30, 0); // 9:30 AM
            DateTime timeClose = candles[candles.Count - 1].End;// new(2024, 01, 03, 16, 0, 0); // 4:00 PM
            TimeSpan timeSpan = TimeSpan.FromMinutes(
                Convert.ToInt16(Settings.GlobalParameters.CandleInterval)
                );
            List<OHLC> prices = new();
            foreach (Candle candle in candles)
            {
                double open = candle.Open;
                double close = candle.Close;
                double high = candle.High;
                double low = candle.Low;

                prices.Add(new OHLC(open, high, low, close, candle.End, timeSpan));
            }
            myPlot.Add.Candlestick(prices);
            myPlot.Axes.DateTimeTicksBottom();

            double onePoint = myPlot.Axes.Bottom.Max - myPlot.Axes.Bottom.Min;
            myPlot.Add.VerticalLine(264);
            myPlot.Add.HorizontalLine(264);

            for (int i = linePoints.Count - 1; i != linePoints.Count - 5; i--)
            {
                var axLine3 = myPlot.Add.Line(
                myPlot.Axes.Bottom.Min,
                       linePoints[i].CoordinateY,
                myPlot.Axes.Bottom.Max,
                       linePoints[i].CoordinateY
                      );
            }
            //foreach (var linePoint in linePoints)
            //{
            //    var axLine3 = myPlot.Add.Line(
            //        myPlot.Axes.Bottom.Min,
            //        linePoint.CoordinateY,
            //        myPlot.Axes.Bottom.Max,
            //        linePoint.CoordinateY
            //       );
            //}


            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);
            return $"{Settings.GlobalParameters.PathSave}\\{nameFile}";
        }






        /// <summary>
        /// Создание картинки свечей
        /// </summary>
        /// <param name="candles">экземпляр класса candles</param>
        /// <param name="nameFile">название файла</param>
        /// <returns></returns>
        static public string GraphicCreateStock(List<Candle> candles, string nameFile = "за день свечки.png")
        {
            ScottPlot.Plot myPlot = new();
            DateTime timeOpen = candles[0].Begin;// new(2024, 01, 03, 9, 30, 0); // 9:30 AM
            DateTime timeClose = candles[candles.Count - 1].End;// new(2024, 01, 03, 16, 0, 0); // 4:00 PM
            TimeSpan timeSpan = TimeSpan.FromMinutes(
                Convert.ToInt16(Settings.GlobalParameters.CandleInterval)
                );
            List<OHLC> prices = new();
            foreach (Candle candle in candles)
            {
                double open = candle.Open;
                double close = candle.Close;
                double high = candle.High;
                double low = candle.Low;

                prices.Add(new OHLC(open, high, low, close, candle.End, timeSpan));
            }
            myPlot.Add.Candlestick(prices);
            myPlot.Axes.DateTimeTicksBottom();

            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);
            return $"{Settings.GlobalParameters.PathSave}\\{nameFile}";
        }


        static public string GraphicCreatorValue(List<Candle> candles, string nameFile = "Объём покупки - продажи.png")
        {
            ScottPlot.Plot myPlot = new();

            // create a bar plot
            double[] values = { 5, 10, 7, 13, 25, 60 };
            myPlot.Add.Bars(values);
            myPlot.Axes.Margins(bottom: 0);
            return "";
        }



        /// <summary>
        /// поиски экстремумов
        /// </summary>
        /// <param name="value"></param>
        private static void SearchForExtremum(double[] value)
        {


            //  double[] newValue = copyToArray(value);
            //  Array.Sort(newValue);
            Dictionary<int, double> extremumsMax = new Dictionary<int, double>();
            Dictionary<int, double> extremumsMin = new Dictionary<int, double>();
            Dictionary<int, double> extremumsGlobalMax = new Dictionary<int, double>();
            Dictionary<int, double> extremumsGlobalMin = new Dictionary<int, double>();

            double temp = 0;
            double temp2 = 0;
            int count = 0;
            int step = 0;
            bool swithcMin = true;
            bool swithcMax = true;

            foreach (double s in value)
            {
                if (Math.Round(s) > temp)
                {
                    temp = Math.Round(s);
                    extremumsMax.Add(step, temp);
                    if (swithcMax)
                    {
                        extremumsGlobalMax.Add(step, Math.Round(s));
                        swithcMax = false;
                        swithcMin = true;
                    }
                    else
                    {
                        extremumsGlobalMax[extremumsGlobalMax.Keys.Max()] = Math.Round(s);
                    }

                }
                if (Math.Round(s) < temp)
                {
                    temp = Math.Round(s);
                    extremumsMin.Add(step, temp);
                    if (swithcMin)
                    {
                        extremumsGlobalMin.Add(step, Math.Round(s));
                        swithcMin = false;
                        swithcMax = true;
                    }
                    else
                    {
                        extremumsGlobalMin[extremumsGlobalMin.Keys.Max()] = Math.Round(s);
                    }
                }

                step++;

            }
            List<double> ds = new List<double>();
            foreach (double s in extremumsGlobalMin.Values)
            {
                ds.Add(s);
            }
            List<double> ds1 = new List<double>();
            foreach (double s in extremumsGlobalMax.Values)
            {
                ds1.Add(s);
            }
            pudo(extremumsGlobalMin.Values.ToArray());

        }





        static private void pudo(double[] collection)
        {
            List<double> temp = new List<double>();
            bool flagAdd = false;
            foreach (double d in collection)
            {
                if (collection.Contains(d + 1) || collection.Contains(d - 1))
                {
                    temp.Add(d);
                }
            }


        }




        private static double[] copyToArray(double[] oldArray)
        {
            double[] newArray = new double[oldArray.Count()];
            int step = 0;
            foreach (double item in oldArray)
            {
                newArray[step] = item;
                step++;
            }
            return newArray;
        }
    }
}
