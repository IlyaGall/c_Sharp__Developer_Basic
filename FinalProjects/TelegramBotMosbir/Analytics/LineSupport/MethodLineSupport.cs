using ObjectsBot;
using RequestParsingMoscowExchange.Parsing;
using System.Reflection.Metadata;


namespace Analytics.LineSupport
{
    /// <summary>
    /// Разбор линий поддержки
    /// </summary>
    public class MethodLineSupport
    {
        #region Линия поддержки для свечки

        /// <summary>
        /// Получить массив линий поддержки и сопротивления
        /// </summary>
        /// <returns>коллекцию линий поддержки</returns>
        public List<LinePoint> lineSupport(List<Candle> candles)
        {
            List<LinePoint> linePoints = new();
            double arraySum = 0;
            for (var i = 0; i < candles.Count; i++)
            {
                var middle = candles[i].High - candles[i].Low;
                arraySum += middle;
            }
            var s = arraySum / candles.Count;
            List<double> levels = new List<double>();
            for (int i = 2; i < candles.Count - 2; i++)
            {
                if (IsSupport(candles, i))
                {
                    double l = candles[i].Low;
                    if (IsFarFromLevel(levels.ToArray(), l, s))
                    {
                        linePoints.Add(new LinePoint(
                            coordinateY: l,
                            coordinateXTime: candles[i].End,
                            idPoint: i - 1,
                            typeLine: 1)
                        );
                    }
                }
                else if (IsResistance(candles, i))
                {
                    double l = candles[i].High;
                    if (IsFarFromLevel(levels.ToArray(), l, s))
                    {

                        linePoints.Add(new LinePoint(
                           coordinateY: l,
                           coordinateXTime: candles[i].End,
                           idPoint: i - 1,
                           typeLine: 2)
                       );
                    }
                }
            }
            return linePoints;

        }

        /// <summary>
        /// Линия поддержки
        /// </summary>
        /// <param name="myCandlesArray">Массив свечек</param>
        /// <param name="i">Позиция элемента в коллекции, который нужно проверить</param>
        /// <returns>true - это линия поддержки, false- это не линия поддержки</returns>
        private bool IsSupport(List<Candle> myCandlesArray, int i) =>
                         myCandlesArray[i].Low < myCandlesArray[i - 1].Low
                         && myCandlesArray[i].Low < myCandlesArray[i + 1].Low
                         && myCandlesArray[i + 1].Low < myCandlesArray[i + 2].Low
                         && myCandlesArray[i - 1].Low < myCandlesArray[i - 2].Low;

        /// <summary>
        /// Линия сопротивления
        /// </summary>
        /// <param name="myCandlesArray"> Массив свечек</param>
        /// <param name="i"> Позиция элемента в коллекции, который нужно проверить</param>
        /// <returns>true-это линия сопротивления, false- это не линия сопротивления</returns>
        private static bool IsResistance(List<Candle> myCandlesArray, int i) =>
                          myCandlesArray[i].High > myCandlesArray[i - 1].High
                          && myCandlesArray[i].High > myCandlesArray[i + 1].High
                          && myCandlesArray[i + 1].High > myCandlesArray[i + 2].High
                          && myCandlesArray[i - 1].High > myCandlesArray[i - 2].High;

        /// <summary>
        /// Убрать шумы по свечке
        /// </summary>
        /// <param name="levels">Количество линий поддержки</param>
        /// <param name="l">Проверяемая линия поддержки</param>
        /// <param name="s">Средняя свеча</param>
        /// <returns>true линию можно добавить, false линию нельзя добавлять</returns>
        static bool IsFarFromLevel(double[] levels, double l, double s)
        {
            foreach (var lev in levels)
            {
                if (Math.Abs(l - lev) <= s)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }

}
