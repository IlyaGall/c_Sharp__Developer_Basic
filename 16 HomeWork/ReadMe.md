# 17 задание "Попрактиковаться в написании SQL запросов"

## 0. Определиться с ORM: Dapper (Linq2Db).

* Dapper - библиотека
* Npgsql - для работы с progresSQL


## 1. Выбрать какую БД использовать (из задания "Sql запросы" или "Кластерный индекс"), написать строку подключения к БД и использовать ее для подключения. (опираться можно на пример из материалов)

* progreSQL
```c#
 /// <summary>
 /// Помощник подключения к серверу
 /// </summary>
 public class HelpConnectionServer
 {
     /// <summary>
     /// Имя сервера
     /// </summary>
     public string NameServer { get; set; } = "localhost";
     /// <summary>
     /// Номер порта
     /// </summary>
     public int PortConnectionServer { get; set; } = 5432;
     /// <summary>
     /// Имя пользователя
     /// </summary>
     public string UserId { get; set; } = "postgres";
     /// <summary>
     /// Пароль от пользователя
     /// </summary>
     public string Password { get; set; } = "";
     /// <summary>
     /// Название БД
     /// </summary>
     public string NameDateBase { get; set; } = "telegramBot";
     /// <summary>
     /// Строка подключения к БД
     /// </summary>
     public string ConnectionString { get; set; } = "";
     /// <summary>
     /// Сформировать строку подключения к БД
     /// </summary>
     public void CreateStringConnection()
     {
         ConnectionString = $"Server={NameServer};Port={PortConnectionServer};User Id={UserId};Password={Password};Database={NameDateBase};";
       
     }

 }
```
в качестве задачи выбрал ["Индексы: кластерный и не кластерный"](https://github.com/IlyaGall/c_Sharp__Developer_Basic/tree/main/15%20HomeWork)


## 2. Создать классы, которые описывают таблицы в БД
 
* в папке "Objects" в проекте

## 3. Используя ORM выполнить простые запросы к каждой таблице, выполнить параметризованные запросы к каждой таблице (без JOIN) - 2-3 запроса на таблицу. 

* простые запросы
* запросы с фильтром
* ввод данных с консоли
* запросы с доболением данных

## 4. Значения параметров для фильтрации можно как задавать из консоли, так и значениями переменных в коде. (пример GetStudent)

* ввод данных с консоли 
* запросы с фильтром

## 5. Выполнить все запросы, из выбранного ранее задания с передачей параметров.

выполнено!





