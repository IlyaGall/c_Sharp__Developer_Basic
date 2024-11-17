using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_homeWork.Objects
{
    public class Product
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Название продукта
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Описание продукта
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Количество на складе
        /// </summary>
        public int stockquantity { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// Вывести параметра в консоль
        /// </summary>
        public void GetConsole()
        {
            Console.WriteLine($"id: {id} - name: {name} - description: {description} - stockquantity: {stockquantity} - price: {price}");
        }

    }
}
