namespace Analytics.MA
{
    /// <summary>
    /// Аналитика данным
    /// </summary>
    public class AnalyticSMA
    {
        /// <summary>
        /// Получить аналитику по SMA
        /// </summary>
        /// <param name="value">Массив с значениями торгов</param>
        /// <returns></returns>
        public string SMA(double[] value)
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
    }
}
