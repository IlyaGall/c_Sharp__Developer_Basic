﻿using ObjectsBot;
using RequestParsingMoscowExchange.Parsing;
using SettingsProject;


namespace RequestParsingMoscowExchange.ListRequest
{
    public static class RequestCommand
    {

        static private string CorrectDateWorkMoscowExchange(int day = 0) 
        {

            DateTime date = DateTime.Now.AddDays(day);
            return date.ToString();

            //DateTime dateTime = new DateTime(2018, 05, 09);
            for (int i = 0; i < 30; i++) 
            {
                date = DateTime.Now.AddDays(day - i);
                //30 дней проверки на работу мосбиржи
                if (CollectionDays.CollectionHollyDay.ContainsKey(date)) 
                {
                    if (CollectionDays.CollectionHollyDay[date].IsWorkDay) 
                    {//если праздничный день, рабочий, например, 02.11.2024
                        return date.ToString();
                    }
                }
                if (CollectionDays.CollectionWeekDay[Convert.ToInt32(date.DayOfWeek)].IsWorkDay) 
                {
                    return date.ToString();
                }
               

            }


            //;  CollectionDays.CollectionWeekDay


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
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        static private string CheckHoliday(string url) 
        {
       
            bool flag =  ParserXML.CheckedDateXML(url);
            int stepDay = 0;
            string newRequst = "";
            while (!flag) 
            {
                stepDay--;
                newRequst = QueryCandle(url, stepDay);
                flag = ParserXML.CheckedDateXML(newRequst);
                if (stepDay < -60) 
                {
                    return "";
                }
            }
            return newRequst;
        }



        /// <summary>
        /// Вернуть информацию по запросу
        /// </summary>
        /// <returns></returns>
        static public string info() 
            => $"/command QUERYLastPrice вернуть последнюю цену по акциям\n" +
                $"/command QUERY2 вернуть последнюю цену по акциям расширенное наименование\n" +
                $"/command QuerySearchName \n" +
                $"/command QueryCandle вернуть свечи\n" +
                $"/command QueryStatisticAll вернуть все акции\n" +
                "/exit выход\n" +
                "/обновить бд - Обновить бд, где храниться весь список не избранных акций\n" +
                "/получение свечи\n" +
                "/индекс мосбиржи";
      
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
        /// Получение свечи за 1 день
        /// </summary>
        /// <param name="nameActive">название актива, например SBER</param>
        /// <param name="addDays">Количество дней</param>
      
        ///  где "interval"интервал свечи (минуты) - задаться через настройки
        /// <returns></returns>
        static public string QueryCandle(string nameActive = "SBER")
        {
            var sdds= $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={dateTime()}&till={dateTime()}&interval={Settings.GlobalParameters.CandleInterval}";
            return sdds;

            string request = $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={dateTime()}&till={dateTime()}&interval={Settings.GlobalParameters.CandleInterval}";
            CorrectDateWorkMoscowExchange();

              request = CheckHoliday(request);
            return request;
        }

        static public string QueryCandle(string nameActive = "SBER", int addDays = 0)
        {
            return  $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={dateTime(addDays)}&till={dateTime(addDays)}&interval={Settings.GlobalParameters.CandleInterval}";

        }


        /// <summary>
        /// получение свечи
        /// </summary>
        /// <param name="nameActive">название актива, например SBER</param>
        /// <param name="dataStart">дата начала торгов (год-месяц-день)</param>
        /// <param name="dataEnd">дата конца торгов (год-месяц-день)</param>
        /// <returns></returns>
        static public string QueryStock(string nameActive = "SBER", string dataStart = "2024-01-01", string dataEnd = "2024-01-30")
            =>   $@"http://iss.moex.com/iss/engines/stock/markets/shares/securities/{nameActive}/candles.xml?iss.meta=off&from={dataStart}&till={dataEnd}&interval={Settings.GlobalParameters.CandleInterval}";
        
        /// <summary>
        /// Получить данные по активу за год
        /// </summary>
        /// <param name="nameActive">Название актива</param>
        /// <returns></returns>
        static public string QueryStockYear(string nameActive = "SBER")
            =>$@"https://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities/{nameActive}.xml?iss.meta=off&iss.only=history|history.cursor&history.columns=CLOSE,TRADEDATE&from={dateTime(-364)}&till={dateTime()}";
       

        /// <summary>
        /// Вернуть все текущие цены по акциям
        /// </summary>
        /// <returns></returns>
        static public string QueryStatisticAll() 
            => @"http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities.json";
      

        /// <summary>
        /// Получить акции с наименованием и тикером для заполнения БД
        /// </summary>
        /// <returns></returns>
        static public string QueryGetAllAction()
           => @"https://iss.moex.com/iss/engines/stock/markets/shares/boards/tqbr/securities.xml?iss.meta=off&iss.only=securities&securities.columns=SECID,PREVPRICE,SHORTNAME";
       
        /// <summary>
        /// Вернуть индекс московской биржи(за 30 дней)
        /// </summary>
        /// <returns></returns>
        static public string QueryGetMoscowExchange()
            => $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE&from={dateTime(-30)}&till={dateTime()}";

        /// <summary>
        /// Вернуть индекс московской биржи(за год)
        /// </summary>
        /// <returns></returns>
        static public string QueryGetMoscowExchangeYear() 
            =>  $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history|history.cursor&history.columns=CLOSE,TRADEDATE&from={dateTime(-364)}&till={dateTime()}";
        
        /// <summary>
        /// Индекс мосбиржи
        /// </summary>
        /// <param name="dataStart">дата начала</param>
        /// <param name="dataEnd">дата окончания</param>
        /// <returns></returns>
        static public string QueryGetMoscowExchange(string dataStart, string dataEnd)
            =>  $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history|history.cursor&history.columns=CLOSE,TRADEDATE&from={dataStart}&till={dataEnd}";
    }
}