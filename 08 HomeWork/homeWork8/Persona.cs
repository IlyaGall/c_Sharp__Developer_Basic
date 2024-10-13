using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork8
{
    /// <summary>
     /// персона
     /// </summary>
    internal class Persona
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// зп сотрудника
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// заполнить данные об одном человеке
        /// </summary>
        /// <param name="name">имя</param>
        /// <param name="salary">зп</param>
        public Persona(string name, double salary)
        {
            Name = name;
            Salary = salary;
        }
    }
}
