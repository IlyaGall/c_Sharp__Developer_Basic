using System;

namespace HomeWork13Links
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("-------Task1------");
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 10; i < 101; i+=5)
            {
                Console.WriteLine($"{i}: {String.Join(" ", list.Top(i))}");
            }

            Console.WriteLine("-------Task2------");
            var collectionPerson = new List<Person>() 
            {
               new Person("Name1", 10),
               new Person("Name2", 100),
               new Person("Name3", 10),
               new Person("Name4", 60),
               new Person("Name5", 55),
               new Person("Name6", 1),
            };
            var result = collectionPerson.Top(50, el => el.Age);
            foreach (var res in result)
            {
                Console.WriteLine(res.Age);
            }
        }
    }
}
