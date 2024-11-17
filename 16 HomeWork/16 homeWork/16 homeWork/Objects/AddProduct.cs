using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _16_homeWork.Objects
{
    public class AddProduct
    {
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
        /// Добавить новый продукт
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="price"></param>
        public AddProduct(string name, string description, int stockQuantity, double price ) 
        {
            this.name = name;
            this.description = description;
            stockquantity = stockQuantity;
            this.price = price;
        }
    }
}
