using Analytics.LineSupport;
using Analytics.TextGeneration;
using RequestParsingMoscowExchange.Parsing;
using ObjectsBot;
using Analytics.EMA;


namespace Analytics
{
    public static class Main
    {
        /// <summary>
        /// Аналитика по индексу мосбиржи
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string AnalyticMoscowExchange(Dictionary<DateTime, double> items)
        {
            int countItems = items.Count;
            string returnStringAnalytic = TextAnalytic.generationText(
                "Индекс мосбиржи",
                $"{countItems.ToString()}",
                 TextAnalytic.calculationsIndex(items, countItems));
            returnStringAnalytic += TextAnalytic.generationText("Индекс мосбиржи", "10", TextAnalytic.calculationsIndex(items, 11));
            returnStringAnalytic += TextAnalytic.generationText("Индекс мосбиржи", "2", TextAnalytic.calculationsIndex(items, 2));
            return returnStringAnalytic;
        }


        /// <summary>
        /// Аналитика по одному активу
        /// </summary>
        /// <param name="url">Url адрес запроса</param>
        /// <param name="nameStock">Название актива</param>
        /// <param name="typeActive">Тип актива(по умолчанию stocks)</param>
        /// <param name="text">Описание периода</param>
        /// <param name="history">Нужно ли подтягивать историю, если она более 100 дней</param>
        /// <returns></returns>
        public static ObjectAnalytics AnalyticMoscowExchangeActive(string url, ObjectAnalytics objectAnalytics, string nameStock = "sber", string typeActive = "stocks", string text = "")
        {
            List<string> items = new List<string>();
            Dictionary<DateTime, double> collectionInformationForCreateImages = new Dictionary<DateTime, double>();

            switch (typeActive)
            {
                case "stocks":
                    var s = ParserXML.passingXMLStock(url);
                    items = new List<string>()
                    {
                        GraphicCreator.Creat.GraphicCreateStock(
                           candles:s,
                           nameFile:nameStock + ".png"
                        )
                    };
                    collectionInformationForCreateImages = ParserXML.parsingXmlHistory(RequestParsingMoscowExchange.ListRequest.RequestCommand.QueryStockYear(nameStock));
                    double[] array1 = collectionInformationForCreateImages.Values.ToArray();
                    objectAnalytics?.PathImg?.Add(
                         GraphicCreator.Creat.GraphicCreatorDateTime(
                         nameFile: $"Индекс за 364 дня.png",
                         collectionInformationForCreateImages.Keys.ToArray(),
                         array1)
                    );

                    objectAnalytics?.PathImg?.Add(
                        GraphicCreator.Creat.GraphicCreatorDateTimeEMA(
                        nameFile: $"Индекс за 364 дня скользящий.png",
                        collectionInformationForCreateImages.Keys.ToArray(),
                        array1,
                        EMA: MovingAverage.EMA(
                            collectionInformationForCreateImages.Values.ToArray(),
                            collectionInformationForCreateImages.Keys.ToArray().Length)
                        )
                   );

                    (string, string) analyzeLineSuppot = GraphicCreator.Creat.GraphicCreatorLineSupport(
                              candles: s,
                              linePoints: MethodLineSupport.lineSupport(url),
                              nameFile: nameStock + ".png"
                             );
                    objectAnalytics?.PathImg?.Add(
                        analyzeLineSuppot.Item1
                        );
                    objectAnalytics?.DescriptionImg?.Add(nameStock + ": " + TextAnalytic.generationText("Свечи", "1", s[0].Open, s[s.Count - 1].Close));
                    objectAnalytics?.DescriptionImg?.Add(analyzeLineSuppot.Item2);

                  //objectAnalytics?.DescriptionImg?.Add(nameStock + ": " + TextAnalytic.generationText("Свечи", "364", s[0].Open, s[s.Count - 1].Close));
                  objectAnalytics.Description = "";
                    return objectAnalytics;
                case "MoscowExchangeHistory": // за 30 дней
                    {
                        collectionInformationForCreateImages = ParserXML.parsingXmlHistory(url);
                        double[] array = collectionInformationForCreateImages.Values.ToArray();
                        objectAnalytics?.PathImg?.Add(

                             GraphicCreator.Creat.GraphicCreatorDateTime(
                             nameFile: $"Индекс за 30 дней.png",
                             collectionInformationForCreateImages.Keys.ToArray(),
                             array)
                        );
                        objectAnalytics?.DescriptionImg?.Add(TextAnalytic.generationText("Индекс мосбиржи", text, array[0], array[array.Length - 1]));
                    }
                    return objectAnalytics;

                case "MoscowExchangeHistoryYear":
                    {
                        collectionInformationForCreateImages = ParserXML.parsingXmlHistory(url);
                        double[] array = collectionInformationForCreateImages.Values.ToArray();
                        objectAnalytics?.PathImg?.Add(
                             GraphicCreator.Creat.GraphicCreatorDateTime(
                             nameFile: $"Индекс за 364 дня.png",
                             collectionInformationForCreateImages.Keys.ToArray(),
                             array)
                        );
                        objectAnalytics?.DescriptionImg?.Add(TextAnalytic.generationText("Индекс мосбиржи", text, array[0], array[array.Length - 1]));
                    }
                    return objectAnalytics;
            }
            //return ("", items, null);
            return objectAnalytics;
        }


    }
}
