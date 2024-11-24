using ObjectsBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.LineSupport
{
    public class AnalyticLine
    {
        /// <summary>
        /// Расчёт линий поддержки для свечного графика
        /// </summary>
        /// <param name="candles">Последняя свеча</param>
        /// <param name="linePoints">Коллекция линий поддержки</param>
        /// <returns>Текст анализа, List<LinePoint>линий поддержки, List<LinePoint> линий сопротивления </returns>
        public (string, Dictionary<double, LinePoint>, Dictionary<double, LinePoint>)
        SupportAndResistance(double candles, List<LinePoint> linePoints)
        {
            List<double> lineSuppot = new List<double>();
            List<double> lineResistance = new List<double>();
            StringBuilder sb = new StringBuilder();
            Dictionary<double, LinePoint> linePointsSupport = new Dictionary<double, LinePoint>();
            Dictionary<double, LinePoint> linePointsResistance = new Dictionary<double, LinePoint>();

            foreach (var linePoint in linePoints)
            {
                if (linePoint.CoordinateY > candles)
                {
                    lineSuppot.Add(linePoint.CoordinateY);
                    if (!linePointsSupport.ContainsKey(linePoint.CoordinateY))
                    {
                        linePointsSupport.Add(linePoint.CoordinateY, linePoint);
                    }
                }
                else
                {
                    lineResistance.Add(linePoint.CoordinateY);
                    if (!linePointsResistance.ContainsKey(linePoint.CoordinateY))
                    {
                        linePointsResistance.Add(linePoint.CoordinateY, linePoint);
                    }
                }
            }
            var sortLinePointsResistance = linePointsResistance.OrderByDescending(k => k.Key).ToDictionary();
            var sortLinePointsSupport = linePointsSupport.OrderBy(k => k.Key).ToDictionary();

            int step = 0;

            if (lineSuppot.Count == 0)
            {
                sb.AppendLine("По текущий данным не получилось найти линии сопротивления.\n");
            }
            else
            {
                sb.AppendLine("Линии поддержки:");
                foreach (var linePoint in sortLinePointsSupport.Keys)
                {
                    if (step > 5) { break; }
                    sb.Append(linePoint + "\n");
                    step++;

                }
            }

            if (lineResistance.Count == 0)
            {
                sb.AppendLine("По текущий данным не получилось найти линии поддержки.\n");
            }
            else
            {
                sb.AppendLine("\nЛинии сопротивления:");
                step = 0;
                foreach (var linePoint in sortLinePointsResistance.Keys)
                {
                    if (step > 5) { break; }
                    sb.Append(linePoint + "\n");
                    step++;
                }
            }
            return (sb.ToString(), sortLinePointsSupport, sortLinePointsResistance);
        }
    }
}
