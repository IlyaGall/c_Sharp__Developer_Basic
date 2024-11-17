using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_homeWork.Objects
{
    public class AddOrder
    {
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
        /// Добавить продукт
        /// </summary>
        /// <param name="customerId">Id позиции</param>
        /// <param name="productId">Id продукта</param>
        /// <param name="quantity">Кол-во</param>
        public AddOrder(int customerId, int productId, int quantity) 
        {
            customerid = customerId;
            productid = productId;
            this.quantity= quantity;
        }


    }
}
