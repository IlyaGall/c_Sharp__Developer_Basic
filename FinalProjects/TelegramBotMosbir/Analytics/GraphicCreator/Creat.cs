using Analytics.EMA;
using Analytics.LineSupport;
using Analytics.MA;
using ObjectsBot;
using ScottPlot;
using SettingsProject;
using System.Text;

namespace Analytics.GraphicCreator
{
    /// <summary>
    /// Отвечает за создание графиков анализа
    /// </summary>
    public class Creat
    {

        /// <summary>
        /// Отрисовать график с ЕМА
        /// </summary>
        /// <param name="nameFile">Название файла, куда будет сохранён график</param>
        /// <param name="date">Массив дат(ось - х)</param>
        /// <param name="value">Массив значений (ось -y)</param>
        /// <param name="EMA">Массив точек ЕMA</param>
        /// <returns></returns>
        static public (string,string) GraphicCreatorDateTimeEMA(string nameFile, DateTime[] date, double[] value, double[] EMA)
        {
            ScottPlot.Plot myPlot = new();
            myPlot.Add.Scatter(date, value); 
            myPlot.Axes.DateTimeTicksBottom();
            myPlot.Axes.Right.MinimumSize = 50;
            myPlot.Add.Scatter(date, EMA); 
            myPlot.Axes.DateTimeTicksBottom();
            AnalyticSMA analyticMA =new AnalyticSMA();
            analyticMA.SMA(value);
            nameFile = nameFile.Insert(nameFile.LastIndexOf('.'), Guid.NewGuid().ToString());
            myPlot.SavePng($"{Settings.GlobalParameters.PathSave}\\{nameFile}", Settings.GlobalParameters.WithIMG, Settings.GlobalParameters.HeightIMG);
            return ($"{Settings.GlobalParameters.PathSave}\\{nameFile}", analyticMA.SMA(value));
        }


        /// <summary>
        /// Нарисовать график с линиями поддержки и сопротивления
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="url"></param>
        /// <param name="nameFile"></param>
        /// <param name="interval"></param>
        /// <param name="lineSupport"></param>
        /// <returns></returns>
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
    }
}
