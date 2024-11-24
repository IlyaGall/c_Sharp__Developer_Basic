using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsBot
{
    /// <summary>
    /// свечка
    /// </summary>
    public class Candle
    {
        /// <summary>
        /// Открытие (цена) 
        /// </summary>
        public double Open { get; set; }
        /// <summary>
        /// Закрытие (цена)
        /// </summary>
        public double Close { get; set; }
        /// <summary>
        /// Покупка(высота)
        /// </summary>
        public double High { get; set; }
        /// <summary>
        /// Продажа (высота)
        /// </summary>
        public double Low { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// Объём
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// Начало время продажи (свечки)
        /// </summary>
        public DateTime Begin { get; set; }
        /// <summary>
        /// Конец время продажи (свечки)
        /// </summary>
        public DateTime End { get; set; }


        public Candle(string? open, string? close, string? high, string? low, string? value, string? volume, string? begin, string? end)
        {
            Open = Convert.ToDouble(open?.Replace('.', ','));
            Close = Convert.ToDouble(close?.Replace('.', ','));
            High = Convert.ToDouble(high?.Replace('.', ','));
            Low = Convert.ToDouble(low?.Replace('.', ','));
            Value = Convert.ToDouble(value?.Replace('.', ','));
            Volume = Convert.ToDouble(volume?.Replace('.', ','));
            if (begin != null) 
            {
                Begin = Convert.ToDateTime(begin);
            }
            if (End != null)
            {
                End = Convert.ToDateTime(end);
            }
        }

    }
}
