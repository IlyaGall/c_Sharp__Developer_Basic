using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsBot
{
   /// <summary>
   /// Коллекция рабочих-выходных дней
   /// </summary>
   static public class CollectionDays
   {
        /// <summary>
        /// Коллекция рабочих/выходных дней
        /// </summary>
        public static Dictionary<int, WeekDay> CollectionWeekDay = new();

        /// <summary>
        /// Празничные дни мосбиржи
        /// </summary>
        public static Dictionary<DateTime, DailyTable> CollectionHollyDay = new();

    }
}
