�������� ������:
https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE
 ���:
* **?iss.meta=off - ���������� ���������� ��� �� �����**
* **&iss.only=history  - �������� ������ ���� ���� history**
* **&history.columns=CLOSE,TRADEDATE - ������� ����������� �������**

*  [������ ��������](https://iss.moex.com/iss/reference/) ������ ��������

������� ��������� ���� �� ���� ������ + ������������ ������
 http://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities.json

 https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities.xml?iss.meta=off&iss.only=marketdata&marketdata.columns=BOARDID,TRADEDATE,SHORTNAME,SECID,NUMTRADES,VALUE,OPEN,LOW,HIGH,LEGALCLOSEPRICE,WAPRICE,CLOSE,VOLUME,MARKETPRICE2,MARKETPRICE3,ADMITTEDQUOTE,MP2VALTRD,MARKETPRICE3TRADESVALUE,ADMITTEDVALUE,WAVAL,TRADINGSESSION,CURRENCYID,TRENDCLSPR
	



	      /* http://iss.moex.com/iss/events
             * 
            ������ �������� � ������� �������, ��� url �������� �� ������� / iss / engines / [engine] / markets / [market] / securities / [security] / candles � ������� �������������� ���������. ������ ������, �������� (� ������� � json):
                      http://iss.moex.com/iss/engines/stock/markets/shares/securities/SBER/candles.xml?iss.meta=off&from=2021-01-01&till=2021-01-30&interval=1
            ������ ��� ������ �� ��������� � 1 �� 30 ������ 2021 ��������.
            */


                 {

            // ��� ����� ������ �� ���� https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history.cursor&from=2024-01-01&till=2024-07-30
            // ��� index + pageSize >= total
            //https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE&from=2024-01-01&till=2024-01-30
           
            return $@"https://iss.moex.com/iss/history/engines/stock/markets/index/boards/SNDX/securities/imoex.xml?iss.meta=off&iss.only=history&history.columns=CLOSE,TRADEDATE&from={dataStart}&till={dataEnd}";
        }