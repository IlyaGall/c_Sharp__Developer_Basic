using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13Links
{
    /*
       1. Создайте дженерик метод расширения для IEnumerable, возвращающий коллекцию, на которой был вызван;(выполнено)
       2. Ограничьте количество элементов выходной коллекции;(выполнено)
       3. Создайте дженерик перегрузку метода Top, добавив для этого одним из параметров функцию, принимающую T и возвращающую int;(выполнено)
       4. Сделайте код-ревью (напишите свой отзыв) на одну из работ других студентов. Ссылки можете попросить в слаке. Для первого студента этот пункт опциональный (хотя и желательный), так как пока нет других работ.
     */
    internal static class EnumerableExtension
    {
        /// <summary>
        /// вернуть массив от чисел
        /// </summary>
        /// <typeparam name="T">Дженерик</typeparam>
        /// <param name="collection">Исходная коллекция</param>
        /// <param name="percent">Нужный процент от коллекции</param>
        /// <returns>Коллекция элементов, которая соответствует percent %(округление в большую сторону)</returns>
        /// <exception cref="ArgumentException">Возникает при вводе percent меньше 1 и больше 100</exception>
        public static IEnumerable<T> Top<T>(this IEnumerable<T> collection, int percent)
        {
            /*
             Напишите свой метод расширения с названием "Top" для коллекции IEnumerable, 
             принимающий значение Х от 1 до 100 и возвращающий заданное количество процентов от выборки с округлением количества 
             элементов в большую сторону.
             То есть для списка var list = new List{1,2,3,4,5,6,7,8,9};
             list.Top(30) должно вернуть 30% элементов от выборки по убыванию значений, то есть [9,8,7] (33%), а не [9,8] (22%).
             Если переданное значение больше 100 или меньше 1, то выбрасывать ArgumentException.
             */
            if (percent < 1 || percent > 100) 
            {
                // согласно ТЗ  "Если переданное значение больше 100 или меньше 1, то выбрасывать ArgumentException."
                throw new ArgumentException($"{percent}% вышел за диапазон от 1 до 100");
            }
            var result = 
                collection.TakeLast // взять последний элемент
                (
                    (int)Math.Ceiling(// возвращаем наименьшее число
                        ((double)collection.Count() * percent) / 100) 
                );
            return result;

        }

        /*
         Напишите перегрузку для метода "Top", которая принимает ещё и поле, по которому будут отбираться топ Х элементов. 
         Например, для var list = new List{...}, вызов list.Top(30, person => person.Age) должен вернуть 30% пользователей с 
         наибольшим возрастом в порядке убывания оного.
         */

        /// <summary>
        /// Вернуть возраст пользователей
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">Коллекция</param>
        /// <param name="percent">Процент возвращаемого кол-ва</param>
        /// <param name="selector">Делегад</param>
        /// <returns></returns>
        public static IEnumerable<T> Top<T>(
         this IEnumerable<T> collection, int percent, Func<T, int> selector) =>
         collection.Top(percent).OrderByDescending(selector);
    }
}
