using System.ComponentModel;
using System.Xml.Linq;

namespace _04_homeWork
{



    /// <summary>
    /// класс стек последний вошёл первый вышел
    /// </summary>
    public class Stack 
    {
        /// <summary>
        /// элемент стека
        /// </summary>
        private class Item 
        {
            /// <summary>
            /// индекс элемента 
            /// </summary>
            public int Index { get; set;}

            /// <summary>
            /// ссылка на предыдущий элемент
            /// </summary>
            public Item ItemAfter { get; set; }
            /// <summary>
            /// значение
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// добавить элемент к стеку
            /// </summary>
            /// <param name="value">значение</param>
            /// <param name="afterItem">ссылка на предыдущий объект стека</param>
            public Item(string value, Item afterItem, int index) 
            {
                Value = value;
                ItemAfter = afterItem;
                Index= index-1;
            }

          

        }

        /// <summary>
        /// стек
        /// </summary>
        private class StackItem
        {
            /*
                Вместо коллекции - создать класс StackItem, который
                доступен только для класс Stack (отдельно объект класса StackItem вне Stack создать нельзя)
                хранит текущее значение элемента стека
                ссылку на предыдущий элемент в стеке
                Методы, описанные в основном задании переделаны под работу со StackItem
             */
            public int Size { get; set; } = 0;
            /// <summary>
            /// текущая ячейка
            /// </summary>
            public Item nowItem { get; set; }
            /// <summary>
            /// предыдущая ячейка
            /// </summary>
            public Item afterItem { get; set; }


            /// <summary>
            /// добавить элемент 
            /// </summary>
            /// <param name="value"></param>
            public void Add(string value)
            {
                if (nowItem == null)
                {
                    // нет ссылки на предыдущий элемент
                    Size = 1;
                    nowItem = new Item(value, null, Size);

                }
                else
                {
                    //есть ссылка на предыдущий элемент
                    afterItem = nowItem;
                    Size++;
                    nowItem = new Item(value, afterItem, Size);

                }
            }

            /// <summary>
            /// вспомогательный метод для отладки стека и вывода его элементов в консоль
            /// </summary>
            public void writeCollection()
            {
                if (nowItem != null && nowItem.Value != null)
                {
                    Console.WriteLine($"[{nowItem.Index}] {nowItem.Value}");
                    //Item? step = nowItem.ItemAfter != null ? nowItem.ItemAfter : null;
                    Item? step;
                    if (nowItem.ItemAfter != null) 
                    {
                        step = nowItem.ItemAfter;
                    }
                    else
                    {
                        step = null;
                    }
                    while (step != null)
                    {
                        if (step != null)
                        {
                            Console.WriteLine($"[{step.Index}] {step.Value}");
                        }
                       // step = nowItem.ItemAfter != null ? step.ItemAfter : null;
                        if (nowItem.ItemAfter != null) 
                        {
                            step = step.ItemAfter; 
                        }
                        else 
                        { 
                            step = null; 
                        }
                    }
                }
            }

            /// <summary>
            /// удаление последнего элемента
            /// </summary>
            public string RemoveLastItem()
            {
                if (nowItem != null)
                {
                    string returnItem = nowItem.Value;
                   // nowItem = nowItem.ItemAfter != null ? nowItem.ItemAfter : null;
                    if (nowItem.ItemAfter != null) 
                    {
                        nowItem = nowItem.ItemAfter;
                    }
                    else 
                    {
                        nowItem = null;
                    }
                    Size--;
                    if (afterItem != null)
                    {
                        afterItem = nowItem.ItemAfter;
                    }
                    return returnItem;
                }
                else
                {
                    Size = 0;
                    throw new Exception("Стек пустой");
                }
            }

            /// <summary>
            /// вывод элемента по индексу
            /// </summary>
            /// <param name="index">элемент</param>
            /// <returns>значение</returns>
            /// <exception cref="OverflowException"></exception>
            public string ElementAt(int index)
            {
               int indexItem = index;
                if (indexItem < 0 || indexItem <= Size)
                {
                    if (nowItem.Index == index)
                    {
                        return nowItem.Value;
                    }
                    else
                    {
                        // Item? step = nowItem.ItemAfter != null ? nowItem.ItemAfter : null;
                        Item? step = null;
                        if (nowItem.ItemAfter != null)
                        {
                             step = nowItem.ItemAfter;
                        }
                        else 
                        {
                             step = null;
                        }
                        while (step != null)
                        {
                            if (step != null && step.Index == indexItem)
                            {
                                return step.Value;
                            }
                          //  step = nowItem.ItemAfter != null ? step.ItemAfter : null;
                            if (nowItem.ItemAfter != null)
                            {
                                step = step.ItemAfter;
                            }
                            else 
                            {
                                step = null;
                            }
                        }
                    }
                }
                throw new OverflowException();
            }
        }

        /// <summary>
        /// экземпляр кастомного стека
        /// </summary>
        StackItem stackItem = new StackItem();

        /// <summary>
        /// кол-во элементов в stack
        /// </summary>
        public int Size { get => stackItem.Size; }

        /// <summary>
        ///последний вошедший элемент в stack
        /// </summary>
        public string Top { get ; set; }

   
        /// <summary>
        /// конструктор Stack
        /// </summary>
        /// <param name="args">массив string</param>
        public Stack(params string[] args)
        {
            foreach (string item in args)
            {
                stackItem.Add(item);
            }
            // stackItem.writeCollection(); 
            TopLastItem();
        }


        /// <summary>
        /// добавление элемента в Stack
        /// </summary>
        /// <param name="item"></param>
        public void Add(string item) 
        {
            stackItem.Add(item);
            TopLastItem();
        }

        /// <summary>
        /// удаление элемента
        /// </summary>
        public string Pop() 
        {
            var returnValue = stackItem.RemoveLastItem();
            TopLastItem();
            return returnValue;
        }

        /// <summary>
        /// скрытая ф-я нахождения последнего элемента
        /// </summary>
        private void TopLastItem() 
        {
           //   Top = stackItem.Size == 0 ? null : stackItem.nowItem.Value;
            if (stackItem.Size == 0)
            {
                Top = null;
            }
            else 
            {
                Top = stackItem.nowItem.Value;
            }
        }

        /// <summary>
        /// Возвращает элемент коллекции по индексу
        /// </summary>
        /// <param name="index">индекс</param>
        /// <returns>элемент string</returns>
        public string ElementAt(int index) 
        {
            return stackItem.ElementAt(index);
        }

        public void writeCollection() 
        {
            stackItem.writeCollection();
        }

        #region Доп. задание 2
        /*
            В класс Stack и добавьте статический метод Concat, который на вход неограниченное количество параметров типа Stack
            и возвращает новый стек с элементами каждого стека в порядке параметров, но сами элементы записаны в обратном порядке
            var s =Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            // в стеке s теперь элементы - "c", "b", "a" "3", "2", "1", "В", "Б", "А" <- верхний
         */
        #endregion
       static public Stack Concat(params Stack[] arrStack) 
       {
            Stack st = new Stack();
            foreach (Stack stack in arrStack) 
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
    public static class StackExtensions
    {
        /// <summary>
        /// добавить к старому стеку новый в перевёрнутом виде
        /// </summary>
        /// <param name="oldStack">ссылка на исходник</param>
        /// <param name="newItem">новая коллекция</param>
        public static void Merge(this Stack oldStack, Stack newItem)
        {
            for (int i = newItem.Size-1; i >= 0; i--) 
            {
                oldStack.Add(newItem.ElementAt(i));
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var s = new Stack("a", "b", "c");


            // size = 3, Top = 'c'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            var deleted = s.Pop();
            // Извлек верхний элемент 'c' Size = 2
            Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
            s.Add("d");
            // size = 3, Top = 'd'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            s.Pop();
            s.Pop();
            s.Pop();
            // size = 0, Top = null
            Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
           //  s.Pop(); // исключение, поэтому закомментировал, чтобы проверить весь код


            var s1 = new Stack("a", "b", "c");
            s1.Merge(new Stack("1", "2", "3"));


            var s2 = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            Console.WriteLine("#############");
            s2.writeCollection();
            Console.ReadKey();
        }
    }
}
