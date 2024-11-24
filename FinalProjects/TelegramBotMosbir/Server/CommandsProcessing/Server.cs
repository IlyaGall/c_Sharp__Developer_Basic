using ObjectsBot;
using RequestParsingMoscowExchange.ListRequest;
using Server.TelegramBot;
using Analytics;
using RequestParsingMoscowExchange.Parsing;

namespace Server.Commands
{
    internal class Server
    {
        /// <summary>
        /// Команды сервера
        /// </summary>
        /// <param name="command">Команда сервера</param>
        /// <param name="nameActive">Названия актива</param>
        /// <returns></returns>
        static public ObjectAnalytics ServerCommand(string command, ObjectAnalytics objectAnalytics, string nameActive = "" )
        {
            Console.WriteLine($"Command client: {command}");
            bool exit = false;
            TelegramBotMessage.SendMessage("введите команду!");
            // string? command = Console.ReadLine();
            switch (command)
            {
                case "/info":
                    objectAnalytics.Description = RequestCommand.info();
                    return objectAnalytics;
                //case "/exit":
                //    exit = true;
                    break;
                case "/indexMB30Day": // индекс московской биржи за 30 дней
                     Analytics.Main.AnalyticMoscowExchangeActive(
                        url: RequestCommand.QueryGetMoscowExchange(),
                        objectAnalytics,
                        typeActive: "MoscowExchangeHistory");
                    return objectAnalytics;
                case "/indexMBYear":
                    Analytics.Main.AnalyticMoscowExchangeActive(
                      url: RequestCommand.QueryGetMoscowExchangeYear(),
                      objectAnalytics,
                      typeActive: "MoscowExchangeHistory");
                    return objectAnalytics;

                case "/AddFavorites":
                    break;

                case "/GetFavoritesStocks":// получить акцию
                    var s = RequestCommand.QueryCandle(nameActive: nameActive);
                    Analytics.Main.AnalyticMoscowExchangeActive(
                        url: RequestCommand.QueryCandle(nameActive: nameActive),
                        objectAnalytics,
                        nameStock: nameActive);
                    return objectAnalytics;
                case "/получить всё акции":
                //    Request.quest("вернуть все акции за сегодняшний день", RequestCommand.QueryStatisticAll());
                    break;
                case "/UpdateBDStock":
                    List<UpdateStockBd> BDStock = ParserXML.ParsingMoscowExchange(RequestCommand.QueryGetFullStock());
                  //  DataBaseTelegramBot 
                    break;
                case "/получение свечи":
                  //  Request.quest("Обновить бд DataStock", RequestCommand.QueryCandle());
                    break;
                case "/индекс мосбиржи":
                    TelegramBotMessage.SendMessage("Введите даты. Две даты через пробел, например 2024-09-23 2024-10-28\n если этого не сделать, то по умолчанию будут взята текущая дата");
                    string? data = Console.ReadLine();

                    if (string.IsNullOrEmpty(data))
                    {
                      //  Request.quest("индекс мосбиржи", RequestCommand.QueryGetMoscowExchange());
                    }
                    else
                    {
                        var splitDate = data.Split(' ');
                        if (splitDate.Length == 2)
                        {
                        //    Request.quest("индекс мосбиржи", RequestCommand.QueryGetMoscowExchange(data.Split(' ')[0], data.Split(' ')[1]));
                        }
                        else
                        {
                           // ErrorProcessing.generateTextError($"Вы ввели не правильный формат данных ''{data}'', а требовалось ''2024-09-23 2024-10-28''");
                        }
                    }
                    break;




                //// индекс мосбиржи https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE&from=2024-01-01&till=2024-01-30

                /////"https://raw.githubusercontent.com/d10xa/holidays-calendar/master/json/calendar.json" #Ссылка на календарь праздников
                /////
                ////  https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.json #индекс мосбиржи
                //// http://iss.moex.com/iss/engines/stock/markets/shares/securities/SBER/candles
                //// http://iss.moex.com/iss/engines/stock/markets/shares/securities/SBER/candles.json?from=2021-01-01&till=2021-01-30&interval=10&securities.columns=SECID,PREVPRICE,SHORTNAME
                //// https://iss.moex.com/iss/engines/stock/markets/shares/boards/tqbr/securities.xml?iss.meta=off&iss.only=securities&securities.columns=SECID,PREVPRICE,SHORTNAME
                default:
                    Console.WriteLine("Ошибка команды!");
                    break;
            }
            return objectAnalytics;

        }
    }
}
