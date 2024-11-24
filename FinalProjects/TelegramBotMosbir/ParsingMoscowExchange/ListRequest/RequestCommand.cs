using ObjectsBot;
using RequestParsingMoscowExchange.Parsing;
using SettingsProject;


namespace RequestParsingMoscowExchange.ListRequest
{
    public static class RequestCommand
    {
        /// <summary>
        /// Проверка, что в этот день московская биржа работает
        /// </summary>
        /// <param name="day">Количество дней, которые нужно прибавить или отнять</param>
        /// <returns></returns>
        static private string CorrectDateWorkMoscowExchange(int day = 0) 
        {
            DateTime date = DateTime.Now.AddDays(day);
            for (int i = 0; i < 30; i++) 
            {
                date = DateTime.Now.AddDays(day - i);
                //30 дней проверки на работу мосбиржи
                if (CollectionDays.CollectionHollyDay.ContainsKey(date)) 
                {
                    if (CollectionDays.CollectionHollyDay[date].IsWorkDay) 
                    {//если праздничный день, рабочий, например, 02.11.2024
                        return date.ToString("yyyy-MM-dd");
                    }
                }
                int dayOfWeek = Convert.ToInt32(date.DayOfWeek) == 0 ? 7: Convert.ToInt32(date.DayOfWeek);

                if (CollectionDays.CollectionWeekDay[dayOfWeek].IsWorkDay) 
                {
                    return date.ToString("yyyy-MM-dd");
                }
            }
            return "";
        }
      
        /// <summary>
        /// Расписание рабочих дней
        /// </summary>
        /// <returns>Запрос</returns>
        static public string TimeTable()
            => "https://iss.moex.com/iss/engines/stock.xml?iss.meta=off&iss.only=timetable";

        /// <summary>
        /// Расписание биржи рабочий день
        /// </summary>
        /// <returns></returns>
        static public string DailyTable()
            => "https://iss.moex.com/iss/engines/stock.xml?iss.meta=off&iss.only=dailytable";

        /// <summary>
        /// Вспомогательный метод для расчёта даты
        /// </summary>
        static private string dateTime(int day = 0)
           => DateTime.Now.AddDays(day).ToString("yyyy-MM-dd");
           

        /// <summary>
        /// Вернуть информацию по запросу
        /// </summary>
        /// <returns></returns>
        static public string info() 
            => "Здравствуйте, я бот.\n\n" +
                "Я умею делать:\n\n" +
                "1) Показывать индекс московской биржи за 30 дней, а так же за 1 год\n\n" +
                "2) Делать простой анализ, включая SMA,EMA а так же искать линии поддержки и сопротивления по акциям и московской бирже.\n\n" +
                "3) С помощь команды /AddFavorites можно добавить актив в избранно и я запомню его, если вы напишите secid правильно!\n" +
                 "Например:\n /AddFavorites SBER\n\n" +
                "4) С помощь команды /getStock можно посмотреть статистику акции и без добавление в избранное LKOH\n" +
                "Например:\n /getStock LKOH\n\n" +
                "5) С помощь команды /UpdateBDStock доступна только моему создателю и я не скажу для чего она! \n\n" +
                
                "Если вам, что-то понадобиться по мимо текущего функционала, можете написать разработчику на почту ilyagall01@gmail.com, но помните, любая доработка будет на коммерческой основе.\n\n" +
                "Хорошего вам настроения и удачный торгов!";


        /// <summary>
        /// Индекс мосбиржи
        /// </summary>
        /// <param name="dataStart">дата начала</param>
        /// <param name="dataEnd">дата окончания</param>
        /// <returns></returns>
        static public string QueryGetMoscowExchange(string dataStart, string dataEnd, string nameActive = "SBER")
        
          => $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={dataStart}&till={dataEnd}&interval={Settings.GlobalParameters.CandleInterval}";


        /// <summary>
        /// Получить всё акции для записи в бд
        /// </summary>
        /// <returns></returns>
        static public string QueryGetFullStock() => @"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=securities&securities.columns=SECID,SHORTNAME,PREVPRICE,LOTSIZE,ISIN";

        /// <summary>
        /// Получение свечи за 1 день
        /// </summary>
        /// <param name="nameActive">Название актива, например SBER</param>
        /// <param name="addDays">Количество дней</param>
        ///  где "interval"интервал свечи (минуты) - задаться через настройки
        /// <returns></returns>
        static public string QueryCandle(string nameActive = "SBER")=>
                     $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={CorrectDateWorkMoscowExchange()}&till={CorrectDateWorkMoscowExchange()}&interval={Settings.GlobalParameters.CandleInterval}";

        /// <summary>
        /// Запрос актива за 1 год
        /// </summary>
        /// <param name="NameActive">Название актива, например SBER</param>
        /// <returns></returns>
        static public string QueryCandleYear(string NameActive= "SBER") =>
            $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{NameActive}/candles.xml?iss.meta=off&from={dateTime(-364)}&till={CorrectDateWorkMoscowExchange()}&interval=10";


        /// <summary>
        /// Получить данные по активу за год
        /// </summary>
        /// <param name="nameActive">Название актива</param>
        /// <returns></returns>
        static public string QueryStockYear(string nameActive = "SBER")
            =>$@"https://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities/{nameActive}.xml?iss.meta=off&iss.only=history|history.cursor&history.columns=CLOSE,TRADEDATE&from={dateTime(-364)}&till={dateTime()}";
       
        /// <summary>
        /// Вернуть индекс московской биржи(за 30 дней)
        /// </summary>
        /// <returns></returns>
        static public string QueryGetMoscowExchange()
              => $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE,OPEN,HIGH,LOW,VALUE&from={dateTime(-31)}&till={dateTime()}";

        /// <summary>
        /// Вернуть индекс московской биржи(за год)
        /// </summary>
        /// <returns></returns>
        static public string QueryGetMoscowExchangeYear()
              => $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE,OPEN,HIGH,LOW,VALUE&from={dateTime(-364)}&till={dateTime()}";

        #region Будующие наработки для дата-майнеров

        /// <summary>
        /// Индекс мосбиржи
        /// </summary>
        /// <param name="dataStart">дата начала</param>
        /// <param name="dataEnd">дата окончания</param>
        /// <returns></returns>
        static public string QueryGetMoscowExchange(string dataStart, string dataEnd)
            =>  $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history|history.cursor&history.columns=CLOSE,TRADEDATE&from={dataStart}&till={dataEnd}";

        /// <summary>
        /// Вернуть последнюю цену по всем акциям
        /// </summary>
        static public string QUERYLastPrice() => "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=marketdata&marketdata.columns=SECID,LAST";

        /// <summary>
        /// Вернуть последнюю цену по всем акциям + наименование полное
        /// </summary> 
        static public string QUERY2() => "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=marketdata&marketdata.columns=BOARDID,TRADEDATE,SHORTNAME,SECID,NUMTRADES,VALUE,OPEN,LOW,HIGH,LEGALCLOSEPRICE,WAPRICE,CLOSE,VOLUME,MARKETPRICE2,MARKETPRICE3,ADMITTEDQUOTE,MP2VALTRD,MARKETPRICE3TRADESVALUE,ADMITTEDVALUE,WAVAL,TRADINGSESSION,CURRENCYID,TRENDCLSPR";

        /// <summary>
        /// Вернуть значения выбранной акции
        /// </summary>
        /// <param name="nameLot">название актива </param>
        /// <returns></returns>
        static public string QuerySearchName(string nameLot = "SBER") => $@"http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities/{nameLot}.json";

        /// <summary>
        /// Вернуть все текущие цены по акциям
        /// </summary>
        /// <returns></returns>
        static public string QueryStatisticAll()
            => @"http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities.json";


        /// <summary>
        /// Получение свечи
        /// </summary>
        /// <param name="nameActive">название актива, например SBER</param>
        /// <param name="dataStart">дата начала торгов (год-месяц-день)</param>
        /// <param name="dataEnd">дата конца торгов (год-месяц-день)</param>
        /// <returns></returns>
        static public string QueryStock(string nameActive = "SBER", string dataStart = "2024-01-01", string dataEnd = "2024-01-30")
            => $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={dataStart}&till={dataEnd}&interval={Settings.GlobalParameters.CandleInterval}";


        #endregion
    }
}
