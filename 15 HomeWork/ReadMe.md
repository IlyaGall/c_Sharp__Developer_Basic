# 15 задание "Попрактиковаться в написании SQL запросов"

## 1 задание
В СУБД PostgreSQL создать БД Shop

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/15%20HomeWork/img/1.JPG)


## 2 задание 
Создать таблицы Customers (ID, FirstName, LastName, Age), Products (ID, Name, Description, StockQuantity, Price) и Orders (ID, CustomerID, ProductID, Quantity)

```sql
---- создаём таблички
CREATE TABLE Customers
(
    ID SERIAL PRIMARY KEY,
    Age INTEGER, 
    FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(50) NOT NULL
);

CREATE TABLE  Products
(
	ID SERIAL PRIMARY KEY,
	Name VARCHAR(20) NOT NULL,
	Description VARCHAR(100) NOT NULL,
	StockQuantity real,
	Price real
);

CREATE TABLE Orders
(
    Id SERIAL PRIMARY KEY,
    CustomerId INTEGER,
	ProductID INTEGER,
    Quantity INTEGER,
    FOREIGN KEY (CustomerId) REFERENCES Customers (ID),
    FOREIGN KEY (ProductID) REFERENCES Products (ID)
);
```

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/15%20HomeWork/img/2.JPG)


## 3 задание 

Установить между ними соответствующие связи по внешним ключам (в каждой таблице поле ID является первичным ключом)


сделано при создании бД

```sql
CREATE TABLE Orders
(
    Id SERIAL PRIMARY KEY,
    CustomerId INTEGER,
	ProductID INTEGER,
    Quantity INTEGER,
    FOREIGN KEY (CustomerId) REFERENCES Customers (ID), -- <- тут сделал  соответствующие связи по внешним ключам 
    FOREIGN KEY (ProductID) REFERENCES Products (ID) -- <- тут сделал  соответствующие связи по внешним ключам 
);
```

## 4 задание 

Заполнить таблицы произвольными значениями (с корректными значениями для внешних ключей). В каждой таблице не менее 10 записей.

```sql
do $do$
begin
	for i in 1..11 loop
		insert into Customers(Age, FirstName, LastName)
			values(i %100,
			concat('Ivan', i::text),
			concat('Ivanov', i::text, 'Random')
	);
	end loop;
end;
$do$;

select * from Customers
```

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/15%20HomeWork/img/3.JPG)


```sql
do $do$
begin
	for i in 1..11 loop
		insert into Products(Name, Description, StockQuantity, Price)
			values(
			concat('Name_', i::text),
			concat('Description_', i::text, '_Description'),
			i %100,
			i * 10			
	);
	end loop;
end;
$do$;

select * from Products
```
![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/15%20HomeWork/img/4.JPG)


```sql

do $do$
begin
	for i in 1..11 loop
		insert into Orders(CustomerId, ProductID, Quantity)
			values(
			i ,
			i ,
			i*12
	);
	end loop;
end;
$do$;

select * from Orders
```

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/15%20HomeWork/img/5.JPG)


## 5 задание

Написать запрос, который возвращает список всех пользователей старше 30 лет, у которых есть заказ на продукт с ID=1. Используйте alias, чтобы дать столбцам в результирующей выборке понятные названия. В результате должны получить таблицу:
CustomerID, FirstName, LastName, ProductID, ProductQuantity, ProductPrice


```sql
insert into Customers (Age, FirstName, LastName)
			values(40,'Ilya','Ivanov'),(41,'Victor','Ivanov') -- <- пришлось добавить записи


insert into Products (Name, Description, StockQuantity, Price)
			values('Огурец','Огурец солённый',1,1000),('Пельмень','1 пельмень',10,1)-- <- пришлось добавить записи

insert into Orders (CustomerId, ProductID, Quantity)
			values(13, 1, 0),(14, 2, 1)-- <- пришлось добавить записи
			
select CustomerID, FirstName, LastName, ProductID, Quantity as ProductQuantity, Price as ProductPrice
from Customers as cus
join Orders as ord on cus.ID = ord.CustomerId
join Products as pr on pr.ID = ord.ProductID
where age > 30 and pr.id=1
```

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/15%20HomeWork/img/6.JPG)

## 6 задание

```sql
create index index_Customers_age
on Customers(Age)
```

!анализ проводил на запросе из 5 задания

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/7.JPG)

![img](https://github.com/IlyaGall/c_Sharp__Developer_Basic/blob/main/14%20HomeWork/img/8.JPG)
