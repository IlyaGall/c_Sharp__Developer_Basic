using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.MA
{
    public class AnalyticMA
    {
        public void MA(double[] value) 
        {
            // Пример данных (цены за дни)
            List<double> prices = new List<double> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            int period = 254; // Период для скользящей средней

            List<double> movingAverages = CalculateMovingAverage(value, period);

            Console.WriteLine("Скользящая Средняя (МА):");
            foreach (var ma in movingAverages)
            {
                Console.WriteLine(ma);
            }
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
