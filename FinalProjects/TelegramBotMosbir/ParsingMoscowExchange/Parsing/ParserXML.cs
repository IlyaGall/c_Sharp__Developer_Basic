using System;
using System.Xml;
using System.Xml.Linq;
using ObjectsBot;
using RequestParsingMoscowExchange.RequestMoscowExchange;

namespace RequestParsingMoscowExchange.Parsing
{
    /// <summary>
    /// Класс для распарсивания XML
    /// </summary>
    public static class ParserXML
    {
        /// <summary>
        /// Загрузка рабочих дней московской биржи
        /// </summary>
        /// <param name="xml"></param>
        static public void LoadDay()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(RequestParsingMoscowExchange.RequestMoscowExchange.Request.request(
                       RequestParsingMoscowExchange.ListRequest.RequestCommand.TimeTable()));
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xNode in xRoot)
                {
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        foreach (XmlNode childNode1 in childNode.ChildNodes)
                        {
                            CollectionDays.CollectionWeekDay.Add(Convert.ToInt32(childNode1?.Attributes?["week_day"].InnerText),
                            new WeekDay(
                                childNode1?.Attributes?["is_work_day"].InnerText,
                                Convert.ToDateTime(childNode1?.Attributes?["start_time"].InnerText),
                                Convert.ToDateTime(childNode1?.Attributes?["stop_time"].InnerText)
                                )
                            );
                        }
                    }
                }
            }
        }
     
        /// <summary>
        /// Загрузка каникул мосбиржи
        /// </summary>
        /// <param name="xml"></param>
        static public void LoadHoliday()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(RequestParsingMoscowExchange.RequestMoscowExchange.Request.request(
                       RequestParsingMoscowExchange.ListRequest.RequestCommand.DailyTable()));
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xNode in xRoot)
                {
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        foreach (XmlNode childNode1 in childNode.ChildNodes)
                        {
                            CollectionDays.CollectionHollyDay.Add(
                                Convert.ToDateTime(childNode1?.Attributes?["date"].InnerText),
                            new DailyTable(
                                childNode1?.Attributes?["is_work_day"].InnerText,
                                childNode1?.Attributes?["start_time"].InnerText,
                                childNode1?.Attributes?["stop_time"].InnerText
                                )
                            );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Получение всех акций и их расшифровка
        /// </summary>
        /// <param name="url"></param>
        static public List<UpdateStockBd> ParsingMoscowExchange(string url) 
        {
            List<UpdateStockBd> updateStockBds = new List<UpdateStockBd>();

            XDocument xmlDoc = new XDocument();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(Request.request(url));
            // Извлекаем данные о securities
            var securities = xmlDoc.Descendants("security");
            XmlElement? xRoot = xDoc.DocumentElement;
            foreach (XmlElement xNode in xRoot)
            {
                foreach (XmlNode childNode in xNode.ChildNodes)
                {
                    foreach (XmlNode childNode1 in childNode.ChildNodes)
                    {
                        if (childNode1.LocalName == "row")
                        {

                            updateStockBds.Add(
                              new UpdateStockBd(
                                childNode1?.Attributes?["SECID"].InnerText,
                                childNode1?.Attributes?["SHORTNAME"].InnerText,
                                childNode1?.Attributes?["PREVPRICE"].InnerText,
                                childNode1?.Attributes?["LOTSIZE"].InnerText,
                                childNode1?.Attributes?["ISIN"].InnerText
                             )
                              );

                        }
                    }
                }
            }

            return updateStockBds;
        }
    
        /// <summary>
        /// Вспомогательный класс для распарсивания данных
        /// </summary>
        /// <param name="items"></param>
        /// <param name="xNode"></param>
        /// <param name="visibleTeg"></param>
        /// <returns></returns>
        static private Dictionary<DateTime, double> parsingXML(Dictionary<DateTime, double> items, XmlElement xNode, bool visibleTeg = false)
        {
            // обходим все дочерние узлы элемента user
            foreach (XmlNode childNode in xNode.ChildNodes)
            {
                foreach (XmlNode childNode1 in childNode.ChildNodes)
                {
                    if (visibleTeg)
                    {
                        for (int i = 0; i < childNode1?.Attributes?.Count; i++)
                        {
                            Console.Write($"{childNode1.Attributes[i].InnerText} {childNode1.Attributes[i].LocalName} ");
                        }
                    }
                    items.Add(Convert.ToDateTime(childNode1?.Attributes?[1].InnerText), Convert.ToDouble(childNode1?.Attributes?[0].InnerText.ToString().Replace(".", ",")));
                    if (visibleTeg)
                    {
                        Console.WriteLine();
                    }
                }
            }
            return items;
        }


        /// <summary>
        /// Парсинг исторического массива данных
        /// </summary>
        /// <returns></returns>
        public static Dictionary<DateTime, double> parsingXmlHistory(string request, bool visibleTeg = false)
        {
            string xml = Request.RequestServer(request);
            Dictionary<DateTime, double> items = new Dictionary<DateTime, double>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xNode in xRoot)
                {
                    // получаем атрибут name
                    XmlNode? attr = xNode.Attributes.GetNamedItem("id");
                    switch (attr?.Value)
                    {
                        case "history":
                            items = parsingXML(
                                items: items,
                                xNode: xNode,
                                visibleTeg: false);
                            break;
                        case "history.cursor":
                            foreach (XmlNode childNode in xNode.ChildNodes)
                            {
                                foreach (XmlNode childNode1 in childNode.ChildNodes)
                                {
                                    double index = Convert.ToInt32(childNode1?.Attributes?[0].InnerText);
                                    double total = Convert.ToInt32(childNode1?.Attributes?[1].InnerText);
                                    double pageSize = Convert.ToInt32(childNode1?.Attributes?[2].InnerText);

                                    if (index + pageSize < total)
                                    {
                                        for (double i = 1; i < total / 100; i++)
                                        {
                                            Console.WriteLine($"обработано страниц {i} из {total / 100}");
                                            xDoc.LoadXml(Request.request(request + $"&start={i * 100}"));
                                            xRoot = xDoc.DocumentElement;
                                            foreach (XmlElement xNodeHistory in xRoot)
                                            {
                                                XmlNode? attrHistory = xNodeHistory.Attributes.GetNamedItem("id");

                                                switch (attrHistory?.Value)
                                                {
                                                    case "history":
                                                        items = parsingXML(
                                                            items: items,
                                                            xNode: xNodeHistory,
                                                            visibleTeg: false);
                                                        break;
                                                }

                                            }
                                        }
                                    }


                                }
                            }
                            break;
                        default:
                            throw new Exception("Не известный xml файл");
                    }
                }
            }

            return items;
        }

        /// <summary>
        /// Парсинг xml для московской биржи
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<Candle> passingXMLMoscowExchange(string request)
        {
            List<Candle> candles = new List<Candle>();
            bool flagStop = false;
            int startRow = 0;
            while (!flagStop)
            {
                flagStop = true;
                string xml = Request.RequestServer(request + $"&start={startRow.ToString()}");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                XmlElement? xRoot = xDoc.DocumentElement;
                foreach (XmlElement xNode in xRoot)
                {
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        foreach (XmlNode childNode1 in childNode.ChildNodes)
                        {
                            if (childNode1.LocalName == "row")
                            {
                                flagStop = false;
                                candles.Add(
                                    new Candle(
                                        childNode1?.Attributes?["OPEN"].InnerText,
                                        childNode1?.Attributes?["CLOSE"].InnerText,
                                        childNode1?.Attributes?["HIGH"].InnerText,
                                        childNode1?.Attributes?["LOW"].InnerText,
                                        childNode1?.Attributes?["VALUE"].InnerText,
                                        null,
                                        childNode1?.Attributes?["TRADEDATE"].InnerText,
                                        childNode1?.Attributes?["TRADEDATE"].InnerText
                                    )
                                );
                            }
                        }
                    }
                }
                startRow += 100;
                Console.WriteLine("ParserXML: " + startRow);
            }
            return candles;
        }


        /// <summary>
        /// Парсинг xml для акций
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<Candle> passingXMLStock(string request)
        {
            List<Candle> candles = new List<Candle>();
            bool flagStop = false;
            int startRow = 0;
            while (!flagStop)
            {
                flagStop = true;
                string xml = Request.RequestServer(request + $"&start={startRow.ToString()}");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xml);
                XmlElement? xRoot = xDoc.DocumentElement;
                foreach (XmlElement xNode in xRoot)
                {
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        foreach (XmlNode childNode1 in childNode.ChildNodes)
                        {
                            if (childNode1.LocalName == "row")
                            {
                                flagStop = false;

                                candles.Add(
                                    new Candle(
                                        childNode1?.Attributes?["open"].InnerText,
                                        childNode1?.Attributes?["close"].InnerText,
                                        childNode1?.Attributes?["high"].InnerText,
                                        childNode1?.Attributes?["low"].InnerText,
                                        childNode1?.Attributes?["value"].InnerText,
                                        childNode1?.Attributes?["volume"].InnerText,
                                        childNode1?.Attributes?["begin"].InnerText,
                                        childNode1?.Attributes?["end"].InnerText
                                    )
                                );
                            }
                        }
                    }
                }
                startRow += 500;
                Console.WriteLine("ParserXML: " + startRow);
            }
            return candles;
        }

    }
}
