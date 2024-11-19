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
        public (string, List<LinePoint>, List<LinePoint>) SupportAndResistance(double candles, List<LinePoint> linePoints) 
        {
            List<double> lineSuppot = new List<double>();
            List<double> lineResistance = new List<double>();
            StringBuilder sb = new StringBuilder();
            List<LinePoint> linePointsSupport= new List<LinePoint>();
            List<LinePoint> linePointsResistance = new List<LinePoint>();


            foreach (var linePoint in linePoints)
            {
                if (linePoint.CoordinateY > candles)
                {
                    lineSuppot.Add(linePoint.CoordinateY);
                    linePointsSupport.Add(linePoint);
                }
                else
                {
                    lineResistance.Add(linePoint.CoordinateY);
                    linePointsResistance.Add(linePoint);
                }
            }
            lineSuppot.Sort();
            lineResistance.Sort();
            int step = 0;
            sb.AppendLine("Линии поддержки:\n");
            if (lineSuppot.Count == 0) 
            {
                sb.AppendLine("По текущий данным не получилось найти линии сопротивления.\n");
            }
            else 
            {
                foreach (var linePoint in lineSuppot)
                {
                    if (step > 5) { break; }
                    sb.Append(linePoint + "\n");
                    step++;

                }
            }
            sb.AppendLine("\nЛинии сопротивления:\n");
            if (lineResistance.Count == 0)
            {
                sb.AppendLine("По текущий данным не получилось найти линии поддержки.\n");
            }
            else
            {
                step = 0;
                foreach (var linePoint in lineResistance)
                {
                    if (step > 5) { break; }
                    sb.Append(linePoint+"\n");
                    step++;
                }
            }
            return (sb.ToString(), linePointsSupport, linePointsResistance);
        }
    }
}
