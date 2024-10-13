using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;


namespace HomeWork13_2.Core
{
    /// <summary>
    /// Библиотека
    /// </summary>
    internal class Library
    {
        /// <summary>
        /// Словарь библиотекаря 
        /// </summary>
        public ConcurrentDictionary<string, int> library = new ConcurrentDictionary<string, int>();

        /// <summary>
        /// Добавление книжки
        /// </summary>
        /// <param name="nameBook">Название книги</param>
        /// <param name="nowRead">На сколько книга прочитана % (в целочисленное число)</param>
        public void AddBooks(string nameBook,int nowRead=0)
        {
            if (!library.ContainsKey(nameBook))
            {
                library.TryAdd(nameBook, nowRead);
            }

        }

        /// <summary>
        /// Запуск бесконечного прочтения книг
        /// </summary>
        public Library()
        {
           // TaskRunReadBook();
        }

        /// <summary>
        /// Запуск потока на удаление книжек
        /// </summary>
        /// <returns></returns>
        async public Task TaskRunReadBook(CancellationTokenSource cst) 
        {
          await Task.Run(ReadBook, cst.Token);
        }

        /// <summary>
        /// Удаление книги из общей коллекции
        /// </summary>
        private void ReadBook()
        {
            while (true)
            {
                Thread.Sleep(1000);      
                foreach (KeyValuePair<string, int> item in library)
                {
                    library.AddOrUpdate(item.Key, 0, (key, oldValue) => oldValue + 1);
                    if (item.Value >= 100)
                    {
                        library.TryRemove(item.Key, out _);
                    }
                }
            }
        }

        /// <summary>
        /// Получить текущие книги и их "прочитанность" 
        /// </summary>
        public void GetListBook() 
        {
            Console.WriteLine("-------- список книг ---------");
            if (library.Count == 0) 
            {
                Console.WriteLine("Всё прочитано!");
            }
            foreach (KeyValuePair<string, int> item in library) 
            {
                Console.WriteLine($"{item.Key} - {item.Value} %");
            }
            Console.WriteLine("-------- ------------ ---------");
        }
    }
}
