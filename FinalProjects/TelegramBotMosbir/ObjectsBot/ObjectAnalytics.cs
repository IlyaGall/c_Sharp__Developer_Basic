using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsBot
{
    /// <summary>
    /// Анализированный ответ для отправки пользователю
    /// </summary>
    public class ObjectAnalytics
    {
        /// <summary>
        /// Описание анализа по активу
        /// </summary>
        public string? Description { get; set; } 

        /// <summary>
        /// Путь к графикам, которые нужно отправить клиенту
        /// </summary>
        public List<string>? PathImg { get; set; }
        /// <summary>
        /// Описание графика в виде текста
        /// </summary>
        public List<string>? DescriptionImg { get; set; }

        public ObjectAnalytics() 
        {
            Description = "";
            PathImg = new List<string>();
            DescriptionImg = new List<string>();
        }

        public void Clear() 
        {
            Description = "";
            PathImg?.Clear();
            DescriptionImg?.Clear();
        }

    }
}
