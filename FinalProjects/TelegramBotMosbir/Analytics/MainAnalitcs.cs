using Analytics.TextGeneration;
using RequestParsingMoscowExchange.Parsing;
using ObjectsBot;
using Analytics.EMA;
using SettingsProject;
using RequestParsingMoscowExchange.ListRequest;

namespace Analytics
{
    public static class MainAnalytics
    {
        /// <summary>
        /// Аналитика по одному активу
        /// </summary>
        /// <param name="url">Url адрес запроса</param>
        /// <param name="nameStock">Название актива</param>
        /// <param name="typeActive">Тип актива(по умолчанию stocks)</param>
        /// <param name="text">Описание периода</param>
        /// <returns></returns>
        public static ObjectAnalytics AnalyticMoscowExchangeActive(string url, ObjectAnalytics objectAnalytics, string nameStock = "sber", string typeActive = "stocks", string text = "")
        {
            Dictionary<DateTime, double> collectionInformationForCreateImages = new Dictionary<DateTime, double>();
            switch (typeActive)
            {
                case "historyStocks":
                    {
                        List<Candle> candles = ParserXML.passingXMLStock(url);
                        (string, string) analyzeLineSuppot = GraphicCreator.Creat.GraphicCreatorLineSupport(
                           candles: candles,
                           url: url,
                           nameFile: nameStock + ".png",
                           interval: Convert.ToInt16(Settings.GlobalParameters.CandleInterval)
                          );
                        objectAnalytics?.PathImg?.Add(analyzeLineSuppot.Item1);
                        objectAnalytics?.DescriptionImg?.Add("Исторические данные:\n"+analyzeLineSuppot.Item2);
                        objectAnalytics.Description = "";
                    }
                    break;

                case "stocks":
                    {
                        List<Candle> candles = ParserXML.passingXMLStock(url);

                        #region Анализ актива по EMA и SMA
                        collectionInformationForCreateImages = ParserXML.parsingXmlHistory(RequestParsingMoscowExchange.ListRequest.RequestCommand.QueryStockYear(nameStock));
                        double[] array1 = collectionInformationForCreateImages.Values.ToArray();
                        (string, string) analyzeEMA = GraphicCreator.Creat.GraphicCreatorDateTimeEMA(
                            nameFile: $"Индекс за 364 дня скользящий.png",
                            collectionInformationForCreateImages.Keys.ToArray(),
                            array1,
                            EMA: MovingAverage.EMA(
                                collectionInformationForCreateImages.Values.ToArray(),
                                collectionInformationForCreateImages.Keys.ToArray().Length));

                        objectAnalytics?.PathImg?.Add(analyzeEMA.Item1);
                        objectAnalytics?.DescriptionImg?.Add(analyzeEMA.Item2);
                        #endregion

                        #region Линии поддержки по акции за 1 день
                        (string, string) analyzeLineSuppot = GraphicCreator.Creat.GraphicCreatorLineSupport(
                                  candles: candles,
                                  url: url,
                                  nameFile: nameStock + ".png",
                                  interval: Convert.ToInt16(Settings.GlobalParameters.CandleInterval)
                                 );
                        objectAnalytics?.PathImg?.Add(analyzeLineSuppot.Item1);
                        objectAnalytics?.DescriptionImg?.Add(nameStock + ": " + TextAnalytic.generationText("Свечи", "1", candles[0].Open, candles[candles.Count - 1].Close));
                        objectAnalytics?.DescriptionImg?.Add(analyzeLineSuppot.Item2);
                        objectAnalytics.Description = "";
                        #endregion

                        #region Статистика за 1 год по активу
                        objectAnalytics?.DescriptionImg?.Add("Статистика за 1 год:");
                        candles = ParserXML.passingXMLStock(RequestCommand.QueryCandleYear(nameStock));

                        analyzeLineSuppot = GraphicCreator.Creat.GraphicCreatorLineSupport(
                              candles: candles,
                              url: url,
                              nameFile: nameStock + ".png",
                              interval: Convert.ToInt16(10),
                              lineSupport: false
                             );
                        objectAnalytics?.PathImg?.Add(analyzeLineSuppot.Item1);
                        objectAnalytics?.DescriptionImg?.Add(nameStock + ": " + TextAnalytic.generationText("Свечи", "за 1 год", candles[0].Open, candles[candles.Count - 1].Close));
                        objectAnalytics.Description = "";
                        #endregion

                    }
                    return objectAnalytics;
                case "MoscowExchangeHistory": // за 30 дней
                case "MoscowExchangeHistoryYear": // за 1 год
                    {
                        List<Candle> Candles = ParserXML.passingXMLMoscowExchange(url);
                        (string, string) analyzeLineSuppot = GraphicCreator.Creat.GraphicCreatorLineSupport(
                                candles: Candles,
                                url: url,
                                nameFile: nameStock + ".png",
                                interval: 1440
                               );

                        objectAnalytics?.PathImg?.Add(analyzeLineSuppot.Item1);
                        objectAnalytics?.DescriptionImg?.Add("Биржа" + ": " + TextAnalytic.generationText("Индекс мосбиржи", $"{Candles.Count - 1}", Candles[0].Open, Candles[Candles.Count - 1].Close));
                        objectAnalytics?.DescriptionImg?.Add(analyzeLineSuppot.Item2);
                        objectAnalytics.Description = "";
                    }
                    return objectAnalytics;
            }
            return objectAnalytics;
        }
    }
}
