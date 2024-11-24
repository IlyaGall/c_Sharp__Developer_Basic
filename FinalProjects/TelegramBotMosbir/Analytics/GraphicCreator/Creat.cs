using Analytics.EMA;
using Analytics.LineSupport;
using Analytics.MA;
using ObjectsBot;
using ScottPlot;
using SettingsProject;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;


namespace Analytics.GraphicCreator
{
    public class Creat
    {

       


        /// <summary>
        /// Построение графика Время - x, диапазон чисел y
        /// </summary>
        /// <param name="directory">пусть сохранения картинки</param>
        /// <param name="nameFile">название файла</param>
        /// <param name="date">массив дат</param>
        /// <param name="value">массив значений</param>
        static public (string, string) GraphicCreatorDateTime(string nameFile, DateTime[] date, double[] value)
        {
            ScottPlot.Plot myPlot = new();
            myPlot.Add.Scatter(date, value); 
            myPlot.Axes.DateTimeTicksBottom();
            myPlot.Axes.Right.MinimumSize = 50;
            StringBuilder stringBuilder = new StringBuilder();
            Analytics.LineSupport.AnalyticLine SupportResistance = new LineSupport.AnalyticLine();
            MethodLineSupport methodLineSupport = new MethodLineSupport();
            List<double> lines = methodLineSupport.lineSupport2(value);

            foreach ( var line in lines) 
            {
                myPlot.Add.HorizontalLine(line);
            }
           

            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);
          
            AnalyticMA analyticMA = new AnalyticMA();
            analyticMA.MA(value);
            var analytics = SupportResistance.SupportAndResistanceArray(value[value.Length - 1], lines);
            stringBuilder.Append(analytics.Item1);
            return ($"{Settings.GlobalParameters.PathSave}\\{nameFile}", stringBuilder.ToString());
        }

        static public (string,string) GraphicCreatorDateTimeEMA(string nameFile, DateTime[] date, double[] value, double[] EMA)
        {
            AnalyticEMA eMAAnalytic = new AnalyticEMA();

            ScottPlot.Plot myPlot = new();
            myPlot.Add.Scatter(date, value); // x , y
            myPlot.Axes.DateTimeTicksBottom();
            myPlot.Axes.Right.MinimumSize = 50;
            myPlot.Add.Scatter(date, EMA); // x , y
            myPlot.Axes.DateTimeTicksBottom();
            eMAAnalytic.AnalyticEMAData( value, EMA, date);
            AnalyticMA analyticMA =new AnalyticMA();
            analyticMA.MA(value);

            // Analytics.MA.AnalyticMA analyticMA =new Analytics.MA.AnalyticMA();
            //   analyticMA.MA(value);

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
            
            
            return ($"{Settings.GlobalParameters.PathSave}\\{nameFile}", analyticMA.MA(value));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="linePoints"></param>
        /// <param name="nameFile"></param>
        /// <returns>1 путь к картинке, 2 анализ</returns>
        static public (string,string) GraphicCreatorLineSupport(List<Candle> candles, string url, string nameFile = "за день свечки.png", int interval = 1, bool lineSupport = true)
        {
            MethodLineSupport methodLineSupport = new MethodLineSupport();
            List<LinePoint> linePoints = methodLineSupport.lineSupport(candles);

            ScottPlot.Plot myPlot = new();
            DateTime timeOpen = candles[0].Begin;
            DateTime timeClose = candles[candles.Count - 1].End;
            TimeSpan timeSpan = TimeSpan.FromMinutes(
                Convert.ToInt16(interval)
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
           // myPlot.Add.VerticalLine(264);
           // myPlot.Add.HorizontalLine(264);

            StringBuilder stringBuilder = new StringBuilder();

            if (lineSupport)
            {
                Analytics.LineSupport.AnalyticLine SupportResistance = new LineSupport.AnalyticLine();

                var analytics = SupportResistance.SupportAndResistance(candles[candles.Count - 1].Close, linePoints);
                stringBuilder.Append(analytics.Item1);
                int step = 0;
                foreach (var lineItem in analytics.Item2.Values)
                {
                    var axLine3 = myPlot.Add.Line(
                        myPlot.Axes.Bottom.Min,
                              lineItem.CoordinateY,
                        myPlot.Axes.Bottom.Max,
                               lineItem.CoordinateY
                              );
                    step++;
                    if (step > 5)
                    {
                        break;

                    }
                }
                step = 0;
                foreach (var lineItem in analytics.Item3.Values)
                {
                    var axLine3 = myPlot.Add.Line(
                        myPlot.Axes.Bottom.Min,
                              lineItem.CoordinateY,
                        myPlot.Axes.Bottom.Max,
                               lineItem.CoordinateY
                              );
                    step++;
                    if (step > 5)
                    {
                        break;
                    }
                }
            }
            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);
            return ($"{Settings.GlobalParameters.PathSave}\\{nameFile}", stringBuilder.ToString());
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
            DateTime timeOpen = candles[0].Begin;
            DateTime timeClose = candles[candles.Count - 1].End;
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
