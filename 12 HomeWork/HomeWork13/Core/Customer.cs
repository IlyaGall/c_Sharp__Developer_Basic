using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13
{
    /*
     В классе Customer должен быть метод OnItemChanged, который будет срабатывать, когда список товаров в магазине обновился.
     В этом методе надо выводить в консоль информацию о том, какое именно изменение произошло
     (добавлен товар с таким-то названием и таким-то идентификатором / удален такой-то товар).
     */

    internal class Customer
    {

        /// <summary>
        /// Оповещение о том, что в магазине что-то изменилось
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="e">Событие оповещение</param>
        public void OnItemChanged(object? sender, NotifyCollectionChangedEventArgs e) 
        {
        
            //как можно по другому преобразовать тип объекта
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        Item item = (Item)e.NewItems[0];
                        Console.WriteLine($"Добавлен товар Id: {item?.Id} {item?.Name} ");
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        Item item = (Item)e.OldItems[0];
                        Console.WriteLine($"Удален товар Id: {item?.Id} {item?.Name}");
                        break;
                    }
                //case NotifyCollectionChangedAction.Replace:
                //    {
                //        Console.WriteLine($"Элемент {e.OldItems[0]} заменен на {e.NewItems[0]}");
                //        break;
                //    }
                //case NotifyCollectionChangedAction.Move:
                //    {
                //        Console.WriteLine($"Перемещен элемент {e.OldItems[0]} из позиции {e.OldStartingIndex} в позицию {e.NewStartingIndex}");
                //        break;
                //    }
                //case NotifyCollectionChangedAction.Reset:
                //    {
                //        Console.WriteLine("Коллекция сброшена");
                //        break;
                //    }
            }
        }
    }
}
