using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_homeWork.Objects
{
    public class Customer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; } 

        /// <summary>
        /// Возраст
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string firstname { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string lastname { get; set; }

        /// <summary>
        /// Вывести параметра в консоль
        /// </summary>
        public void GetConsole() 
        {
            Console.WriteLine($"id: {id} - age: {age} - firstname: {firstname} - lastname: {lastname}");
        }
    }
}
