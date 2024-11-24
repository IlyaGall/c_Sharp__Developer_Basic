using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.MA
{
    public class AnalyticMA
    {
        public string MA(double[] value)
        {
            double lastValue = value[value.Count() - 1];
            double d = value.Aggregate((x, y) => x + y) / value.Count();
            if (lastValue > d)
            {
                return "Актив стоит больше среднего согласно методике SMA, его можно продавать\n";
            } 
            if (lastValue < d)
            {
                return "Актив стоит меньше среднего согласно методике SMA, его можно покупать\n";
            }
            if (lastValue == d) 
            {
                return "О чудо! Актив стоит столько сколько согласно методике SMA,нужно подождать движения вверх или вниз!\n";

            }
            return null;

        }
        static List<double> CalculateMovingAverage(double[] prices, int period)
        {
            List<double> movingAverages = new List<double>();

            for (int i = 0; i <= prices.Length - period; i++)
            {
                double sum = 0;

                // Суммируем значения за указанный период
                for (int j = 0; j < period; j++)
                {
                    sum += prices[i + j];
                }

                // Вычисляем среднее значение
                double average = sum / period;
                movingAverages.Add(average);
            }

            return movingAverages;
        }
    }
}
