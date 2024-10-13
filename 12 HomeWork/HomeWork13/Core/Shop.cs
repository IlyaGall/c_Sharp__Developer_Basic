using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace HomeWork13
{
    /*
     В классе Shop должна храниться информация о списке товаров (экземпляры классов Item). 
     Также в классе Shop должны быть методы Add (для добавления товара) и Remove (для удаления товара).
     */
    /// <summary>
    /// Магазин
    /// </summary>
    internal class Shop
    {

        /// <summary>
        /// Товары в магазине
        /// </summary>
        ObservableCollection<Item> items = new ObservableCollection<Item>();

        /// <summary>
        /// Подписка пользователя на уведоления
        /// </summary>
        /// <param name="customer"></param>
        public void ChangedShop(Customer customer) 
        {
            items.CollectionChanged += customer.OnItemChanged;// ChangedHandler;
        }
        


        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="name">Название товара</param>
        public void Add(string name) 
        {
            items.Add(new Item(items.Count,name));
           
        }

        /// <summary>
        /// Получить весь список товаров
        /// </summary>
        public void GetListItem()
        {
            foreach (var item in items) 
            {
                 Console.WriteLine($"{item.Id} - {item.Name}");
            }
        }


        /// <summary>
        /// Удаление товара по идентификатору 
        /// </summary>
        /// <param name="idIndex">Id товара</param>
        public void Remove(int idIndex)
        {
            if (items.Count-1 >= idIndex && idIndex >= 0)
            {
                items.RemoveAt(idIndex);
            }
            else
            {
                Console.WriteLine("Такого индекса нетЪ!!!");
            }

        }
    }
}
