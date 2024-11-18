using System;
using System.Xml;
using System.Xml.Linq;
using ObjectsBot;
using RequestParsingMoscowExchange.RequestMoscowExchange;

namespace RequestParsingMoscowExchange.Parsing
{

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
                            CollectionDays.CollectionWeekDay.Add(Convert.ToInt32(childNode1?.Attributes?[0].InnerText),
                            new WeekDay(
                                childNode1?.Attributes?[1].InnerText,
                                Convert.ToDateTime(childNode1?.Attributes?[2].InnerText),
                                Convert.ToDateTime(childNode1?.Attributes?[3].InnerText)
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
                            CollectionDays.CollectionHollyDay.Add(Convert.ToDateTime(childNode1?.Attributes?[0].InnerText),
                            new DailyTable(
                                childNode1?.Attributes?[1].InnerText,
                                childNode1?.Attributes?[2].InnerText,
                                childNode1?.Attributes?[3].InnerText
                                )
                            );
                        }
                    }
                }
            }
        }
        

        /// <summary>
        /// Распрасить данные из json (для заполнения бд  DataStock)
        /// </summary>
        /// <param name="json">файл json</param>
        /// <param name="items">коллекция класса List<DataBase.DataStock></param>
        //static public List<DataBase.DataStock> Parsing(string json, List<DataBase.DataStock> items)
        //{

        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.LoadXml(json);
        //    XmlElement? xRoot = xDoc.DocumentElement;
        //    if (xRoot != null)
        //    {
        //        // обход всех узлов в корневом элементе
        //        foreach (XmlElement xnode in xRoot)
        //        {
        //            // получаем атрибут name
        //            XmlNode? attr = xnode.Attributes.GetNamedItem("row");
        //            Console.WriteLine(attr?.Value);

        //            // обходим все дочерние узлы элемента user
        //            foreach (XmlNode childNode in xnode.ChildNodes)
        //            {
        //                foreach (XmlNode childNode1 in childNode.ChildNodes)
        //                {
        //                    DataBase.DataStock item = new DataBase.DataStock();
        //                    PropertyInfo[] properties = typeof(DataBase.DataStock).GetProperties(); // получение свойства класса
        //                    Type t = typeof(DataBase.DataStock); // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo.propertytype?view=net-8.0


        //                    for (int i = 0; i < childNode1?.Attributes?.Count; i++)
        //                    {
        //                        Console.Write($"{childNode1.Attributes[i].InnerText} {childNode1.Attributes[i].LocalName} ");

        //                        switch (t.GetProperties()[i].PropertyType.Name)
        //                        {
        //                            case "String":
        //                                properties[i].SetValue(item, childNode1.Attributes[i].Value);
        //                                break;
        //                            case "Double":
        //                                properties[i].SetValue(item, Convert.ToDouble(childNode1.Attributes[i].Value.Replace(".", ",")));
        //                                break;
        //                            case "Int16":
        //                                properties[i].SetValue(item, Convert.ToInt16(childNode1.Attributes[i].Value));
        //                                break;
        //                            case "Int32":
        //                                properties[i].SetValue(item, Convert.ToInt32(childNode1.Attributes[i].Value));
        //                                break;
        //                            case "Int64":
        //                                properties[i].SetValue(item, Convert.ToInt64(childNode1.Attributes[i].Value));
        //                                break;
        //                            default:
        //                                throw new Exception($"Не известный тип данных {t.GetProperties()[i].PropertyType.Name}");
        //                        }
        //                    }
        //                    Console.WriteLine("", "");
        //                    items.Add(item);
        //                }
        //            }
        //            Console.WriteLine();
        //        }
        //    }
        //    return items;
        //}



        /// <summary>
        /// Проверить дату, что запрос вернёт не пустой запрос, так как мосбиржа не рабоает
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public bool CheckedDateXML(string url)
        {
            bool flag = false;
            string xml = Request.RequestServer(url);
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
                    Console.WriteLine(attr?.Value);
                    foreach (XmlNode childNode in xNode.ChildNodes)
                    {
                        foreach (XmlNode childNode1 in childNode.ChildNodes)
                        {
                            flag = true;
                        }
                    }
                }
            }

            return flag;
        }


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
        /// распарcить индекс мосбиржи
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="request"></param>
        /// <param name="visibleTeg"></param>
        /// <returns></returns>
        static public string Parsing(string xml, string request, bool visibleTeg = false)
        {
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

                    Console.WriteLine(attr?.Value);

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
                                    //Telegram.SendMessageDebagger($"{childNode1?.Attributes?[0].InnerText} {childNode1?.Attributes?[0].LocalName}\n" +
                                    //     $"{childNode1?.Attributes?[1].InnerText} {childNode1?.Attributes?[1].LocalName}\n" +
                                    //     $"{childNode1?.Attributes?[2].InnerText} {childNode1?.Attributes?[2].LocalName} ");
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

            //  return Analytic.AnalyticMoscowExchange(items);
            return null;
        }



        ////////////////////########новый парсер###########/////////////////////

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
        /// проверить, данные на превышение 500 значений в запросе
        /// </summary>
        /// <returns></returns>
        private static bool DataVerificationCandle(string request)
        {

            return false;
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
                                        childNode1?.Attributes?[0].InnerText,
                                        childNode1?.Attributes?[1].InnerText,
                                        childNode1?.Attributes?[2].InnerText,
                                        childNode1?.Attributes?[3].InnerText,
                                        childNode1?.Attributes?[4].InnerText,
                                        childNode1?.Attributes?[5].InnerText,
                                        childNode1?.Attributes?[6].InnerText,
                                        childNode1?.Attributes?[7].InnerText
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
