﻿using HomeWork13_2.Core;
using HomeWork13_2.UI;

namespace HomeWork13_2
{
    /*
     (2) Напишите программу "Библиотекарь". Суть:
     Пользователю в консоли показывают меню: "1 - добавить книгу; 2 - вывести список непрочитанного; 3 - выйти"
     Если он вводит 1, то далее ему пишут "Введите название книги:". 
     Пользователь вводит название - книга запоминается в коллекции. В качестве коллекции стоит использовать 
     ConcurrentDictionary<string, int> (для чего нужен int - см.далее). 
     Если книга с таким названием уже была добавлена ранее - не добавлять и не обновлять ее.
     Автоматически возвращаемся в меню (снова выводим его в консоль).

     Если вводит 2 - на экран выводится список всех ранее введенных книг и в конце - опять меню
     Если вводит 3 - выходим из программы.

     В выводимом списке книг надо выводить не только их названия, но и вычисленный процент, насколько она прочитана. 
     Например: "Остров сокровищ - 15%".
   
     Для расчета процентов создаем второй поток, который в фоне постоянно перевычисляет проценты. 
     Между каждой итерацией перевычисления он спит 1 секунду. 
     Во время итерации перевычисления он берет коллекцию всех книг и по каждой вычисляет новый процент путем прибавления 1% к предыдущему значению 
     (изначально 0%). Если дошли до 100% - удаляем эту книгу из списка.

     Таким образом, когда пользователь вызовет вывод списка он может получить что-то вроде:
     Любовь к жизни - 45%
     Приключения Мюнхгаузена - 17%
     Незнайка в Солнечном городе - 4%
     */
    internal class Program
    {
        static  void Main(string[] args)
        {
            UI.UI.Menu();
        }
    }
}