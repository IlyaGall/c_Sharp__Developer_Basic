using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13_3.Parts
{
    internal class Part1 
    {
        private readonly string _poemPart = "Вот дом,\r\nКоторый построил Джек.";
        public ImmutableList<string> Poem { get; private set; }
        public Part1(ImmutableList<string> poem)
        {
            Poem = poem;
           
        }

    
        /*
         В каждом классе PartN есть метод AddPart, который на вход принимает коллекцию строк, добавляет в нее новые строки и 
         сохраняет получившуюся коллекцию в свойство "Poem". Требуется это делать так, чтобы исходная коллекция не изменилась.
         Какие именно строки добавляет каждый класс посмотрите здесь - 
         https://russkaja-skazka.ru/dom-kotoryiy-postroil-dzhek-stihi-samuil-marshak/
         (например Part3 добавляет третий параграф стихотворения)

        Надо создать экземпляры этих классов, а затем последовательно вызвать каждый из методов AddPart, передавай в него результат 
        вызова предыдущего метода,
        примерно так: 'MyPart3(MyPart2.Poem)'
        В конце работы программы надо вывести значение свойства "Poem" у каждого из классов и убедиться,
        что изменяя коллекцию в одном классе Вы не затрагивали коллекцию в предыдущем.
         */
        public ImmutableList<string> AddPart(ImmutableList<string> prevPoem)
        {
            Poem = Poem.AddRange(prevPoem);
            return Poem;
        }
    }
}
