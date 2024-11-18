using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsBot
{
    public class DailyTable
    {

        /// <summary>
        /// Рабочий или выходной день московской биржи
        /// </summary>
        public bool IsWorkDay { set; get; }

        /// <summary>
        /// Начало торгов
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Конец торгов
        /// </summary>
        public DateTime StopTime { get; set; }
        public DailyTable(string isWorkDay, string startTime, string stopTime)
        {
            IsWorkDay = isWorkDay == "1" ? true : false;
            StartTime = startTime == "" ? DateTime.Now : Convert.ToDateTime(startTime);
            StopTime = stopTime == "" ? DateTime.Now : Convert.ToDateTime(stopTime);
            // Некоторые даты в xml пустые, поэтому поставил дефолтные значения
        }
    }
}
