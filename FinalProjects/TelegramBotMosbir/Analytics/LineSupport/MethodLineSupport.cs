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
        #region Линия поддержки для не свечки 
        /// <summary>
        /// Линия поддержки для обычного значения без свечки
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<double> lineSupport2(double[] value)
        {
            double s = value.Aggregate((x, y) => x + y) / value.Length;
            List<double> linePoints = new();
            List<double> levels = new List<double>();

            for (int i = 2; i < value.Length - 2; i++)
            {
                if (IsSupportArray(value, i))
                {
                    double l = value[i];
                    if (IsFarFromLevel(levels.ToArray(), l, s))
                    {
                        levels.Add(l);
                        linePoints.Add(value[i]);
                    }
                }
                else if (IsResistanceArray(value, i))
                {
                    double l = value[i];

                    if (IsFarFromLevel(levels.ToArray(), l, s))
                    {
                        levels.Add(l);
                        linePoints.Add(value[i]);
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
        private bool IsSupportArray(double[] myCandlesArray, int i) =>
                         myCandlesArray[i] < myCandlesArray[i - 1]
                         && myCandlesArray[i] < myCandlesArray[i + 1]
                         && myCandlesArray[i + 1] < myCandlesArray[i + 2]
                         && myCandlesArray[i - 1] < myCandlesArray[i - 2];

        /// <summary>
        /// Линия сопротивления
        /// </summary>
        /// <param name="myCandlesArray"> Массив свечек</param>
        /// <param name="i"> Позиция элемента в коллекции, который нужно проверить</param>
        /// <returns>true-это линия сопротивления, false- это не линия сопротивления</returns>
        private bool IsResistanceArray(double[] myCandlesArray, int i) =>       
                         myCandlesArray[i] > myCandlesArray[i - 1]
                         && myCandlesArray[i] > myCandlesArray[i + 1]
                         && myCandlesArray[i + 1] > myCandlesArray[i + 2]
                         && myCandlesArray[i - 1] > myCandlesArray[i - 2];
        #endregion


        #region Линия поддержки для свечки

        /// <summary>
        /// получить массив линий поддержки
        /// </summary>
        /// <returns>коллекцию линий поддержки</returns>
        public List<LinePoint> lineSupport(List<Candle> candles)
        {
            List<double> linePoints1 = new();

            //List<Candle> candles = ParserXML.passingXMLStock(uri);
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
                    linePoints1.Add(l);
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
                    linePoints1.Add(l);
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
        private bool IsSupport(List<Candle> myCandlesArray, int i)=>
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
       private static bool IsResistance(List<Candle> myCandlesArray, int i)=>
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
