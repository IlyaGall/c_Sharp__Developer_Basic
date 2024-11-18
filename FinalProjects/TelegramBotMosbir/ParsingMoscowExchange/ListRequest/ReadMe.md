Ќазвание строки:
https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE
 √де:
* **?iss.meta=off - отключение метаданных они не нужны**
* **&iss.only=history  - оставить только один блок history**
* **&history.columns=CLOSE,TRADEDATE - вывести определЄнные колонки**

*  [список запросов](https://iss.moex.com/iss/reference/) список запросов

вернуть последнюю цену по всем акци€м + наименование полное
 http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities.json

 https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=marketdata&marketdata.columns=BOARDID,TRADEDATE,SHORTNAME,SECID,NUMTRADES,VALUE,OPEN,LOW,HIGH,LEGALCLOSEPRICE,WAPRICE,CLOSE,VOLUME,MARKETPRICE2,MARKETPRICE3,ADMITTEDQUOTE,MP2VALTRD,MARKETPRICE3TRADESVALUE,ADMITTEDVALUE,WAVAL,TRADINGSESSION,CURRENCYID,TRENDCLSPR
	



	      /* http://iss.moex.com/iss/events
             * 
            читаем описание в котором сказано, что url строитс€ по шаблону / iss / engines / [engine] / markets / [market] / securities / [security] / candles и описаны дополнительные параметры. —троим ссылку, например (с выводом в json):
                      http://iss.moex.com/iss/engines/stock/markets/shares/securities/SBER/candles.xml?iss.meta=off&from=2021-01-01&till=2021-01-30&interval=1
            ƒанные дл€ свечей по —бербанку с 1 по 30 €нвар€ 2021 получены.
            */


                 {

            // тут новый запрос по дате https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history.cursor&from=2024-01-01&till=2024-07-30
            // где index + pageSize >= total
            //https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE&from=2024-01-01&till=2024-01-30
           
            return $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE&from={dataStart}&till={dataEnd}";
        }