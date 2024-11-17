using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16_homeWork.Objects
{
    public class AddCustomer
    {
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

       public AddCustomer(int age, string firstName, string lastName)
       {
            this.age = age;
            firstname = firstName;
            lastname = lastName;
       }

    }
}
