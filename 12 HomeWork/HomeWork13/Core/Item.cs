using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13
{
    /*
  В классе Item должны быть свойства Id (идентификатор товара) и Name (название товара).
  */
    internal class Item
    {
        /// <summary>
        /// идентификатор товара
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// название товара
        /// </summary>
        public string? Name { get; private set; }

        /// <summary>
        /// создать товар
        /// </summary>
        /// <param name="id">Индификатор товара</param>
        /// <param name="name">Название товара</param>
        public Item(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
