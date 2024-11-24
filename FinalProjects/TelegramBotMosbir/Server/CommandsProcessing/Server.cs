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
        static public ObjectAnalytics 
            ServerCommand(string command, ObjectAnalytics objectAnalytics, string nameActive = "",string dateStart="", string dateEnd="" )
        {
            Console.WriteLine($"Command client: {command}");
            bool exit = false;
            switch (command)
            {
                case "/info":
                    objectAnalytics.Description = RequestCommand.info();
                    return objectAnalytics;
                case "/indexMB30Day": // индекс московской биржи за 30 дней
                     Analytics.MainAnalytics.AnalyticMoscowExchangeActive(
                        url: RequestCommand.QueryGetMoscowExchange(),
                        objectAnalytics,
                        typeActive: "MoscowExchangeHistory");
                    return objectAnalytics;
                case "/indexMBYear":// индекс московской биржи за 364 дней
                    Analytics.MainAnalytics.AnalyticMoscowExchangeActive(
                      url: RequestCommand.QueryGetMoscowExchangeYear(),
                      objectAnalytics,
                      typeActive: "MoscowExchangeHistory");
                    return objectAnalytics;
                case "/GetFavoritesStocksWithDate":
                    Analytics.MainAnalytics.AnalyticMoscowExchangeActive(
                       url: RequestCommand.QueryGetMoscowExchange(
                           dataStart: dateStart,
                           dataEnd: dateEnd,
                           nameActive: nameActive),
                       objectAnalytics,
                       nameStock: nameActive,
                       typeActive: "historyStocks"
                       );
                    break;
                case "/GetFavoritesStocks":// получить акцию
                   
                    Analytics.MainAnalytics.AnalyticMoscowExchangeActive(
                        url: RequestCommand.QueryCandle(nameActive: nameActive),
                        objectAnalytics,
                        nameStock: nameActive);
                    return objectAnalytics;
                case "/UpdateBDStock"://Обновить базу данных с акциями
                    List<UpdateStockBd> BDStock = ParserXML.ParsingMoscowExchange(RequestCommand.QueryGetFullStock());
                    break;
                default:
                    Console.WriteLine("Ошибка команды!");
                    break;
            }
            return objectAnalytics;

        }
    }
}
