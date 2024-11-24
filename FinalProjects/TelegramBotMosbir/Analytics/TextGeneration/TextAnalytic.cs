
namespace Analytics.TextGeneration
{
    static public class TextAnalytic
    {

        /// <summary>
        /// Позиция индекса за период 
        /// </summary>
        /// <returns>сгенерированный текст ответа</returns>
        public static string generationText(string switcher, string info, params double[] statistic)
        {
            string returnStringAnalytic = "";
            switch (switcher)
            {
                case "Индекс мосбиржи":

                    double percent = statistic[1] * 100 / statistic[0] - 100;
                    if (percent == 0)
                    {
                        returnStringAnalytic = $"Позиция по индексу мосбиржи за {info} дней НЕЙТРАЛЬНАЯ Стоимость: {statistic[1]}\n %: {percent}\n";
                    }
                    if (percent > 0)
                    {
                        returnStringAnalytic = $"Настроение игроков по индексу мосбиржи за {info} дней ПОКУПКА Стоимость: {statistic[1]}\n %: ▲{percent}\n";
                    }
                    if (percent < 0)
                    {
                        returnStringAnalytic = $"Настроение игроков по индексу мосбиржи за {info} дней ПРОДАЖА. Стоимость: {statistic[1]}\n %: ▼{percent}\n";
                    }
                    break;

                case "Свечи":
                    percent = statistic[1] * 100 / statistic[0] - 100;
                    if (percent == 0)
                    {
                        returnStringAnalytic = $"Позиция по за {info} день НЕЙТРАЛЬНАЯ цена: {statistic[1]}\n %: {percent}\n";
                    }
                    if (percent > 0)
                    {
                        returnStringAnalytic = $"Позиция по акции за {info} день ПОКУПКА цена: {statistic[1]}\n %: ▲{percent}\n";
                    }
                    if (percent < 0)
                    {
                        returnStringAnalytic = $"Позиция по акции за {info} день ПРОДАЖА цена: {statistic[1]}\n %: ▼{percent}\n";
                    }
                    break;
            }

            //  GraphicCreator.GraphicCreatorDateTime(path:);

            return returnStringAnalytic;
        }


        /// <summary>
        /// вспомогательная ф-я для расчёта раз
        /// </summary>
        /// <param name="items"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static double[] calculationsIndex(Dictionary<DateTime, double> items, int days)
        {
            Dictionary<DateTime, double> itemsGraphics = new Dictionary<DateTime, double>();
            double statistic = 0;
            double newValueStatistic = 0;
            double oldValueStatistic = 0;
            double startValue = 0;
            double endValue = 0;
            int countItems = items.Count;
            for (int i = countItems - days; i < countItems; i++)
            {
                if (oldValueStatistic == 0)
                {
                    startValue = items.ElementAt(i).Value;
                    oldValueStatistic = items.ElementAt(i).Value;
                    itemsGraphics.Add(items.ElementAt(i).Key, items.ElementAt(i).Value);
                }
                else
                {
                    newValueStatistic = items.ElementAt(i).Value;
                    itemsGraphics.Add(items.ElementAt(i).Key, items.ElementAt(i).Value);
                    statistic += newValueStatistic - oldValueStatistic;
                    oldValueStatistic = items.ElementAt(i).Value;
                }

                if (i == countItems - 1)
                {
                    endValue = items.ElementAt(i).Value;
                }
            }
            //GraphicCreator.GraphicCreatorDateTime(
            //    //directory: Settings.GlobalParameters.PathSave,
            //    nameFile: $"{(countItems - days).ToString()}.png",
            //    itemsGraphics.Keys.ToArray(),
            //    itemsGraphics.Values.ToArray());

            double[] returnArr = new double[3];
            returnArr[0] = statistic;
            returnArr[1] = startValue;
            returnArr[2] = endValue;
            return returnArr;

        }

      
    }
}
