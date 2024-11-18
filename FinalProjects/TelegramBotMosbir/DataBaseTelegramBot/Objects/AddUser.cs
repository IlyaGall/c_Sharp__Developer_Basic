using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTelegramBot.Objects
{
    internal class AddUser
    {
        /// <summary>
        /// ID телеграмм пользователя
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя пользователя в телеграм
        /// </summary>
        public string Name { get; set; }

        public AddUser() { }

    }

}
