using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsBot
{
    /// <summary>
    /// Объект день недели - расписания мосбиржи
    /// </summary>
    public class WeekDay
    {
        /// <summary>
        /// Рабочий/выходной день
        /// </summary>
        public bool IsWorkDay { get; set; }
        /// <summary>
        /// Начало торгов
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Конец торгов
        /// </summary>
        public DateTime StopTime { get; set; }

        public WeekDay( string isWorkDay, DateTime startTime, DateTime stopTime)
        {
            IsWorkDay = isWorkDay == "1" ? true : false;
            StartTime = startTime;
            StopTime = stopTime;
        }
    }
}
