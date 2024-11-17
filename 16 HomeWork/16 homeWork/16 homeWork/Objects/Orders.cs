using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_homeWork.Objects
{
    public class Orders
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Id позиции
        /// </summary>
        public int customerid { get; set; }
        /// <summary>
        /// Id продукта
        /// </summary>
        public int productid { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int quantity { get; set; }
       
        /// <summary>
        /// Вывести параметра в консоль
        /// </summary>
        public void GetConsole()
        {
            Console.WriteLine($"id: {id} - customerid: {customerid} - productid: {productid} - quantity: {quantity}");
        }
    }
}
