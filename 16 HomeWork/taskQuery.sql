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


-- Заполнить таблицы произвольными значениями (с корректными значениями для внешних ключей). 
-- В каждой таблице не менее 10 записей.


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
-----------------

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


-----------------

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


-- Написать запрос, который возвращает список всех пользователей старше 30 лет, у которых есть заказ на 
-- продукт с ID=1. Используйте alias, чтобы дать столбцам в результирующей выборке понятные названия.
-- В результате должны получить таблицу:
-- CustomerID, FirstName, LastName, ProductID, ProductQuantity, ProductPrice


insert into Customers (Age, FirstName, LastName)
			values(40,'Ilya','Ivanov'),(41,'Victor','Ivanov')


insert into Products (Name, Description, StockQuantity, Price)
			values('Огурец','Огурец солённый',1,1000),('Пельмень','1 пельмень',10,1)

insert into Orders (CustomerId, ProductID, Quantity)
			values(13, 1, 0),(14, 2, 1)
			
select CustomerID, FirstName, LastName, ProductID, Quantity as ProductQuantity, Price as ProductPrice
from Customers as cus
join Orders as ord on cus.ID = ord.CustomerId
join Products as pr on pr.ID = ord.ProductID
where age > 30 and pr.id=1

-- Убедитесь, что вы повесили необходимый некластерный индекс (он не особо нужен, когда у вас 10 записей,
-- но пригодится, если бы их было 1000)

create index index_Customers_age
on Customers(Age) 