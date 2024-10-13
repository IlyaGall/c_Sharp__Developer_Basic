using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork13Links
{
    internal class Person
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
       /// <summary>
       /// Возраст
       /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Конструктор класса Person
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="age">Возраст</param>
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
