namespace Analytics.TextGeneration
{
    /// <summary>
    /// Вспомогательный класс для аналитики по индексу
    /// </summary>
    static public class TextAnalytic
    {
        /// <summary>
        /// Позиция индекса за период 
        /// </summary>
        /// <returns>Сгенерированный текст ответа</returns>
        public static string generationText(string switcher, string info, params double[] statistic)
        {
            string returnStringAnalytic = "";
            switch (switcher)
            {
                case "Индекс мосбиржи":
                    double percent = statistic[1] * 100 / statistic[0] - 100;
                    if (percent == 0)
                    {
                        return returnStringAnalytic = $"Позиция по индексу мосбиржи за {info} дней НЕЙТРАЛЬНАЯ Стоимость: {statistic[1]}\n %: {percent}\n";
                    }
                    if (percent > 0)
                    {
                        return returnStringAnalytic = $"Настроение игроков по индексу мосбиржи за {info} дней ПОКУПКА Стоимость: {statistic[1]}\n %: ▲{percent}\n";
                    }
                    if (percent < 0)
                    {
                        return returnStringAnalytic = $"Настроение игроков по индексу мосбиржи за {info} дней ПРОДАЖА. Стоимость: {statistic[1]}\n %: ▼{percent}\n";
                    }
                    break;
                case "Свечи":
                    percent = statistic[1] * 100 / statistic[0] - 100;
                    if (percent == 0)
                    {
                        return returnStringAnalytic = $"Позиция по за {info} день НЕЙТРАЛЬНАЯ цена: {statistic[1]}\n %: {percent}\n";
                    }
                    if (percent > 0)
                    {
                        return returnStringAnalytic = $"Позиция по акции за {info} день ПОКУПКА цена: {statistic[1]}\n %: ▲{percent}\n";
                    }
                    if (percent < 0)
                    {
                        return returnStringAnalytic = $"Позиция по акции за {info} день ПРОДАЖА цена: {statistic[1]}\n %: ▼{percent}\n";
                    }
                    break;
            }
            return returnStringAnalytic;
        }
    }
}
