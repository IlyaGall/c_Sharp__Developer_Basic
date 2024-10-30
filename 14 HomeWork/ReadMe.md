# 14 задание "Попрактиковаться в написании SQL запросов"

## 1 и 2 задание 
1 Установить на локальный компьютер PostgreSQL (см. ссылки в презентации). 
* !ссылки не было в презентации
2 Скачать и восстановить БД dvdrental с помощью pgAdmin (см. ссылки в презентации). Приложить скриншот с pgAdmin.

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/1-2.jpg)

## 3 задание 

В pgAdmin (или Navicat) написать SQL запрос, который вернет названия и описания всех фильмов, продолжительность которых больше 100.
```sql
select title, description from film where length >100
```

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/3.JPG)

## 4 задание 

4 В pgAdmin (или Navicat) написать SQL запрос, который вернет уникальные имена (без фамилий) актеров.

```sql
select DISTINCT(first_name) from actor

```
![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/4.JPG)


## 5 задание

В pgAdmin (или Navicat) написать SQL запрос, который вернет рейтинг фильма и количество фильмов с таким рейтингом, но только для тех рейтингов, которые содержат букву "G".  
```sql
select rating, count(rating) from film where cast(rating as CHAR(35)) LIKE '%G%' group by rating
```
![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/5.JPG)

## 6 задание

В pgAdmin (или Navicat) написать SQL запрос, который вернет имена и фамилии только тех актеров,  которые снимались менее, чем в 20 фильмах. Приложить скриншот с SQL запросом и результирующей выборкой.

```sql
select FIRST_NAME,LAST_NAME from actor
join
(select count(actor_id) as act, film_id from film_actor group by film_id having count(actor_id)<20)
on actor_id= act
```
![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/6.JPG)
