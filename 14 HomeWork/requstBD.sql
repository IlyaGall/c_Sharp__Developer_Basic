---3 В pgAdmin (или Navicat) написать SQL запрос, который вернет названия и описания всех фильмов, 
-- продолжительность которых больше 100. Приложить скриншот с SQL запросом и результирующей выборкой.
select title, description from film where length >100

-- 4 В pgAdmin (или Navicat) написать SQL запрос, который вернет уникальные имена (без фамилий) актеров.
select DISTINCT(first_name) from actor

-- 5 В pgAdmin (или Navicat) написать SQL запрос, который вернет рейтинг фильма и количество фильмов 
-- с таким рейтингом, но только для тех рейтингов, которые содержат букву "G". 
-- Приложить скриншот с SQL запросом и результирующей выборкой.

select rating, count(rating) from film where cast(rating as CHAR(35)) LIKE '%G%' group by rating

--6 В pgAdmin (или Navicat) написать SQL запрос, который вернет имена и фамилии только тех актеров, 
-- которые снимались менее, чем в 20 фильмах. Приложить скриншот с SQL запросом и результирующей выборкой.

select FIRST_NAME,LAST_NAME from actor
join
(select count(actor_id) as act, film_id from film_actor group by film_id having count(actor_id)<20)
on actor_id= act
