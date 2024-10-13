using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_homeWork
{

    public class Stack12
    {



        /// <summary>
        /// кол-во элементов в stack
        /// </summary>
        public int Size;

        /// <summary>
        ///последний вошедший элемент в stack
        /// </summary>
        public string Top;

        List<string> myStack;
        /// <summary>
        /// конструктор Stack
        /// </summary>
        /// <param name="args">массив string</param>
        public Stack12(params string[] args)
        {





            myStack = new List<string>();
            foreach (string element in args)
            {
                myStack.Add(element);
            }
            Size = args.Length;
            TopLastItem();
        }
        /// <summary>
        /// добавление элемента в Stack
        /// </summary>
        /// <param name="item"></param>
        public void Add(string item)
        {
            myStack.Add(item);
            Size++;
            TopLastItem();
        }

        /// <summary>
        /// удаление элемента
        /// </summary>
        /// <exception cref="Exception">при пустом стеке выбрасывает исключение "Стек пустой"</exception>
        public string Pop()
        {
            string lastItem = "";
            if (myStack.Count > 0)
            {
                lastItem = myStack.Last();
                myStack.RemoveAt(myStack.Count - 1);
                Size--;
                TopLastItem();
            }
            else
            {
                TopLastItem();
                Size = 0;
                throw new Exception("Стек пустой");
            }

            return lastItem;
        }
        /// <summary>
        /// скрытая ф-я нахождения последнего элемента
        /// </summary>
        private void TopLastItem()
        {
            Top = myStack.Count == 0 ? null : myStack.Last();
        }

        /// <summary>
        /// Возвращает элемент коллекции по индексу
        /// </summary>
        /// <param name="index">индекс</param>
        /// <returns>элемент string</returns>
        public string ElementAt(int index)
        {
            return myStack[index];
        }


        #region Доп. задание 2
        /*
            В класс Stack и добавьте статический метод Concat, который на вход неограниченное количество параметров типа Stack
            и возвращает новый стек с элементами каждого стека в порядке параметров, но сами элементы записаны в обратном порядке
            var s =Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            // в стеке s теперь элементы - "c", "b", "a" "3", "2", "1", "В", "Б", "А" <- верхний
         */
        #endregion
        static public Stack12 Concat(params Stack12[] arrStack)
        {
            Stack12 st = new Stack12();
            foreach (Stack12 stack in arrStack)
            {
                st.Merge(stack);
            }
            return st;
        }
    }

    #region Доп.задание 1
    /*
    Создайте класс расширения StackExtensions и добавьте в него метод расширения Merge, который на вход принимает стек s1, и стек s2.
    Все элементы из s2 должны добавится в s1 в обратном порядке
    Сам метод должен быть доступен в класс Stack
    var s = new Stack("a", "b", "c")
    s.Merge(new Stack("1", "2", "3"))
    // в стеке s теперь элементы - "a", "b", "c", "3", "2", "1" <- верхний
    */
    #endregion
    /// <summary>
    /// класс расширения
    /// </summary>
    public static class StackExtensions12
    {
        /// <summary>
        /// добавить к старому стеку новый в перевёрнутом виде
        /// </summary>
        /// <param name="oldStack">ссылка на исходник</param>
        /// <param name="newItem">новая коллекция</param>
        public static void Merge(this Stack12 oldStack, Stack12 newItem)
        {
            for (int i = newItem.Size - 1; i >= 0; i--)
            {
                oldStack.Add(newItem.ElementAt(i));
            }
        }
    }
    


    internal class task12
    {
        //1 и 2 задание без изменение стурктуры, чтобы остался пример работы со стеком
    }
}
