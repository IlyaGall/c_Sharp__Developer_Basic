using HomeWork13_3.Parts;
using System.Collections.Immutable;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace HomeWork13_3
{
    /*
        (3) Напишите программу "Дом, который построил Джек".
        В программе должна быть коллекция строк. Каждая строка - строка стихотворения "Дом, который построил Джек".
        Изначально коллекция пустая

        Также в программе есть 9 классов - Part1, Part2, Part3, ..., Part9
    
        В каждом классе PartN есть метод AddPart, который на вход принимает коллекцию строк, 
        добавляет в нее новые строки и сохраняет получившуюся коллекцию в свойство "Poem". 

        Требуется это делать так, чтобы исходная коллекция не изменилась. 
        
        Какие именно строки добавляет каждый класс посмотрите здесь - https://russkaja-skazka.ru/dom-kotoryiy-postroil-dzhek-stihi-samuil-marshak/ 
        (например Part3 добавляет третий параграф стихотворения)

        Надо создать экземпляры этих классов, а затем последовательно вызвать каждый из методов AddPart, передавай в него результат 
        вызова предыдущего метода,
        примерно так: 'MyPart3(MyPart2.Poem)'
        В конце работы программы надо вывести значение свойства "Poem" у каждого из классов и убедиться,
        что изменяя коллекцию в одном классе Вы не затрагивали коллекцию в предыдущем.
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            var part1 = new Part1(ImmutableList<string>.Empty.AddRange(new[] { "Вот дом,\r\nКоторый построил Джек." }));
            var part2 = new Part2(part1.Poem);
            var part3 = new Part3(part2.Poem);
            var part4 = new Part4(part3.Poem);
            var part5 = new Part5(part4.Poem);
            var part6 = new Part6(part5.Poem);
            var part7 = new Part7(part6.Poem);
            var part8 = new Part8(part7.Poem);
            var part9 = new Part9(part8.Poem);
           
            foreach (var part in part9.Poem) 
            {
                Console.WriteLine(part);
            }
          
        }
    }
}
