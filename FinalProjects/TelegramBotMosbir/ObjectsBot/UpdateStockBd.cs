using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsBot
{
    /// <summary>
    /// Объект для обновления БД
    /// </summary>
    public class UpdateStockBd
    {
        /// <summary>
        /// Сид - короткое название позиции
        /// </summary>
        public string SECID { get; set; }
        /// <summary>
        /// Полное название позиции
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Цена лота
        /// </summary>
        public double Prevprice { get; set; }


        /// <summary>
        /// Сколько в одном лоте акций
        /// </summary>
        public int LotSize { get; set; }


        /// <summary>
        /// ISIN акции - код
        /// </summary>
        public string ISIN { get; set; }

        public UpdateStockBd(string SECID, string shortName, string prevprice, string lotSize, string ISIN) 
        {
            this.SECID = SECID;
            ShortName = shortName;
            Prevprice = Convert.ToDouble(prevprice.Replace(".",","));
            LotSize = Convert.ToInt32( lotSize);
            this.ISIN = ISIN;

        }
    }
}
